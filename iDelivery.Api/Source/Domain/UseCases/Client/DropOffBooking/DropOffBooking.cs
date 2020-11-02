using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using iDelivery.Api.Source.Infrastructure.Helpers;
using iDelivery.Api.Source.Infrastructure.Helpers.EmailNotification;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class DropOffBooking : IRequest<DropOffBookingDto>
    {
        private const string RECEIPT_PREFIX = "ORD";

        public int BookingId { get; set; }
        public int RiderId { get; set; }

        public DropOffBooking(int bookingId, int RiderId)
        {
            this.BookingId = bookingId;
            this.RiderId = RiderId;
        }

        private class RequestHandler : IRequestHandler<DropOffBooking, DropOffBookingDto>
        {
            private readonly IEmailSender emailSender;
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper, IEmailSender emailSender)
            {
                this.context = context;
                this.mapper = mapper;
                this.emailSender = emailSender;
            }

            public async Task<DropOffBookingDto> Handle(DropOffBooking request, CancellationToken cancellationToken)
            {
                var booking = await context.Bookings.FindAsync(request.BookingId);
                if (booking == null)
                    throw new NotFoundException();

                var bookingStatus = await context.BookingStatus.FindAsync(booking.BookingStatusId);
                if (bookingStatus == null)
                    throw new NotFoundException();

                if (bookingStatus.BookingStatusName.ToLower() == "cancelled")
                    throw new CancelledBookingIsNotAllowedException();

                if (bookingStatus.BookingStatusName.ToLower() != "to ship")
                    throw new OnlyStatusToShipIsAllowedException();

                if (booking.RiderId != request.RiderId)
                    throw new RiderIsNotAllowedException();

                var status = await context.BookingStatus.SingleOrDefaultAsync(o => o.BookingStatusName.ToLower() == "delivered");
                if (status != null)
                    booking.BookingStatusId = status.BookingStatusId;

                booking.DropOffTime = DateTime.UtcNow;
                // receipt number.
                if (!string.IsNullOrEmpty(booking.ReferenceNumber))
                    booking.ReceiptNumber = $"{RECEIPT_PREFIX}-{booking.ReferenceNumber}";

                var result = mapper.Map<DropOffBookingDto>(booking);

                context.Update(booking);
                await context.SaveChangesAsync();

                // log in booking history
                await LogRiderBookingHistory(request, booking);

                if (status != null)
                    result.BookingStatusName = status.BookingStatusName;

                await DeductPoints(booking, result);

                // send email receipt.
                var customer = await context.Customers.SingleOrDefaultAsync(o => o.UserId == booking.CustomerId);
                if (customer != null)
                    await SendEmailReceipt(customer.ActivateEmailReceipts, customer.Email, booking);

                result.CompletionTime = result.DropOffTime.GetValueOrDefault().Subtract(result.PickupTime.GetValueOrDefault());

                return result;
            }

            private async Task<bool> SendEmailReceipt(bool isActivateEmailReceipts, string email, Booking booking)
            {
                var isEmailSent = default(bool);
                if (isActivateEmailReceipts && !string.IsNullOrEmpty(email))
                {
                    var content = await GetEmailContent(booking);
                    var emailMessage = new EmailMessage("admin@api-idelivery.com", email, content);
                   
                    isEmailSent = await emailSender.SendEmailAsync(emailMessage);
                }

                return isEmailSent;
            }

            private async Task DeductPoints(Booking booking, DropOffBookingDto result)
            {
                // deduct points in wallet.
                var rider = await context.Riders.FindAsync(booking.RiderId);
                if (rider != null)
                {
                    var wallet = await context.Wallets.SingleOrDefaultAsync(o => o.RiderId == rider.RiderId);
                    if (wallet == null)
                        throw new WalletIsNotExistException();

                    // todo: throw exception if the current points is not enough
                    if (wallet.CurrentPoints > 0)
                    {
                        var deduction = booking.TotalFare * 20 / 100;
                        if (deduction > wallet.CurrentPoints)
                            throw new CurrentPointsNotEnoughException();

                        wallet.CurrentPoints = wallet.CurrentPoints - deduction.GetValueOrDefault();
                        var riderFare = booking.TotalFare - deduction;
                        result.RiderFare = riderFare.GetValueOrDefault().ToString("0.00");
                        result.RiderDeduction = deduction.GetValueOrDefault().ToString("0.00");
                    }

                    context.Update(wallet);
                    await context.SaveChangesAsync();
                }
            }

            private async Task LogRiderBookingHistory(DropOffBooking request, Booking booking)
            {
                var riderBookingHistory = new RiderBookingHistory();
                var rider = await context.Riders.SingleOrDefaultAsync(o => o.UserId == request.RiderId);
                if (rider != null) riderBookingHistory.RiderId = rider.RiderId;

                riderBookingHistory.BookingStatusId = booking.BookingStatusId;
                var customer = await context.Customers.SingleOrDefaultAsync(o => o.UserId == booking.CustomerId);
                if (customer != null)
                {
                    riderBookingHistory.CustomerName = $"{ customer.LastName }, {customer.FirstName}";
                    riderBookingHistory.CustomerNumber = customer.ContactNumber;
                }
                riderBookingHistory.DropOffLocation = booking.DropOffLocation;
                riderBookingHistory.PickupLocation = booking.PickupLocation;
                riderBookingHistory.ItemDetails = booking.Items;
                riderBookingHistory.ReceiverName = booking.ContactName;
                riderBookingHistory.ReceiverNumber = booking.ContactNumber;
                riderBookingHistory.BookingDate = booking.BookingDate;
                riderBookingHistory.TotalFare = booking.TotalFare.GetValueOrDefault();
                var fare = await context.Fares.FindAsync(booking.FareId);
                if (fare != null)
                {
                    riderBookingHistory.RiderShares = fare.RidersPercentage;
                    riderBookingHistory.TotalKilometers = booking.TotalKilometers;
                }

                context.RiderBookingHistories.Add(riderBookingHistory);
                await context.SaveChangesAsync();
            }

            private int GetKilometer(string kilometer)
            {
                var b = string.Empty;
                var val = 0;
                for (int i = 0; i < kilometer.Length; i++)
                {
                    if (Char.IsDigit(kilometer[i]))
                        b += kilometer[i];
                }

                if (b.Length > 0)
                    val = int.Parse(b);

                return val;
            }

            private async Task<string> GetEmailContent(Booking booking)
            {
                if (booking == null)
                    return string.Empty;

                var rider = await context.Riders.SingleOrDefaultAsync(o => o.UserId == booking.RiderId);
                var riderName = rider != null ? $"{ rider.LastName }, {rider.FirstName}" : string.Empty;

                var welcomeNote = @"<html>
                                <body>
                                <p>Greetings from i-delivery Team,</p>
                                <br />
                                <p>Thank you for using i-Delivery!</p>";

                var bodyNote = @"<br />
                                <p>Delivery Booking Receipt</p>
                                <p><b>Order Receipt:</b>       " + booking.ReceiptNumber + "</p>" +
                                 "<br/>" +
                                 "<p><b>Rider Name:</b>        " + riderName + "</p>" +
                                 "<p><b>Pick-up Location:</b>  " + booking.PickupLocation + "</p>" +
                                 "<p><b>Drop-off location:</b> " + booking.DropOffLocation + "</p>" +
                                 "<p><b>Item Description:</b>  " + booking.Items + "</p>" +
                                 "<p><b>Total Fare:</b>        " + booking.TotalFare + "</p>" +
                                 "<p><b>Date:</b>              " + booking.BookingDate + "</p>";

                var footerNote = @"<br />
                                <p>---------------------------------------------------------------------------------------</p>
                                <p>This is a system generated email. Please do not reply.</p>
                                <br/>
                                <p>Connect with us! follow-i-Delivery on FACEBOOK https://www.facebook.com/iDeliveryman. If you have any questions, send an email to customersupport@i-delivery.ph or call us on 0906-062-0413(globe) or 0928-422-1861(smart)</p>
                                <br />
                                <p>Copyright C i-Delivery. All rights Reserved</p>
                                </body>
                                </html>";

                return string.Format("{0}{1}{2}", welcomeNote, bodyNote, footerNote);
            }
        }
    }
}
