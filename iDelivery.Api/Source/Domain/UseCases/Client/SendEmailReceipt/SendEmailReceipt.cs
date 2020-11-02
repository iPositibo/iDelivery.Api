using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Infrastructure.Helpers.EmailNotification;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client.SendEmail
{
    public class SendEmailReceipt : IRequest<bool>
    {
        public string Email { get; }

        public SendEmailReceipt(string email) => this.Email = email;

        private class VerifyCodeHandler : IRequestHandler<SendEmailReceipt, bool>
        {
            private readonly IEmailSender emailSender;
            private DataContext context;
            private IMapper mapper;

            public VerifyCodeHandler(DataContext context, IMapper mapper, IEmailSender emailSender)
            {
                this.context = context;
                this.mapper = mapper;
                this.emailSender = emailSender;
            }

            public async Task<bool> Handle(SendEmailReceipt request, CancellationToken cancellationToken)
            {
                var booking = new Booking();
                booking.RiderId = 6;
                booking.ReceiptNumber = "TEST123";
                booking.PickupLocation = "DASMA";
                booking.DropOffLocation = "IMUS";
                booking.Items = "TEST DATA";
                booking.TotalFare = 400;
                booking.BookingDate = DateTime.UtcNow;

                var isEmailSent = default(bool);
                var content = await GetEmailContent(booking);
                var emailMessage = new EmailMessage("admin@api-idelivery.com", request.Email, content);
                try
                {
                    isEmailSent = await emailSender.SendEmailAsync(emailMessage);
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }

                return isEmailSent;
            }

            private async Task<string> GetEmailContent(Booking booking)
            {
                if (booking == null)
                    return string.Empty;

                var rider = await context.Riders.SingleOrDefaultAsync(o => o.RiderId == booking.RiderId);
                var riderName = rider != null ? $"{ rider.LastName }, {rider.FirstName}" : string.Empty;

                var welcomeNote = @"<html>
                                <body>
                                <p>Greetings from i-delivery Team,</p>
                                <br />
                                <p>Thank you for using i-Delivery!</p>";

                var bodyNote = @"<br />
                                <p>Delivery Booking Receipt</p>
                                <p><b>Order Receipt:</b>      " + booking.ReceiptNumber + "</p>" +
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
