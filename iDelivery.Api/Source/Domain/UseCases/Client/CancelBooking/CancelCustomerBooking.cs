using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class CancelCustomerBooking : IRequest<CancelBookingDto>
    {
        public int BookingId { get; set; }

        public CancelCustomerBooking(int bookingId) => this.BookingId = bookingId;

        private class RequestHandler : IRequestHandler<CancelCustomerBooking, CancelBookingDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CancelBookingDto> Handle(CancelCustomerBooking request, CancellationToken cancellationToken)
            {
                var booking = await context.Bookings.FindAsync(request.BookingId);
                if (booking == null)
                    throw new NotFoundException();

                var status = await context.BookingStatus.SingleOrDefaultAsync(o => o.BookingStatusName.ToLower() == "cancelled");
                if (status != null)
                {
                    booking.BookingStatusId = status.BookingStatusId;
                    context.Update(booking);
                    context.SaveChanges();
                    await LogHistory(booking);
                }

                return mapper.Map<CancelBookingDto>(booking);
            }

            private async Task LogHistory(Booking booking)
            {
                var customer = await context.Customers.SingleOrDefaultAsync(o => o.UserId == booking.CustomerId);
                if (customer == null)
                    throw new NotFoundException();

                // log in booking history
                var customerBookingHistory = new CustomerBookingHistory();
                customerBookingHistory.CustomerId = customer.CustomerId;
                customerBookingHistory.BookingStatusId = booking.BookingStatusId;
                customerBookingHistory.ReceiverCompleteName = booking.ContactName;
                customerBookingHistory.ReceiverCompleteAddress = booking.DropOffLocation;
                customerBookingHistory.EstimatedTime = booking.EstimatedTime;
                customerBookingHistory.ItemDetails = booking.Items;
                customerBookingHistory.TotalKilometers = booking.TotalKilometers;
                customerBookingHistory.Receipt = booking.ReferenceNumber;
                customerBookingHistory.BookingDate = booking.BookingDate;
                customerBookingHistory.TotalFare = booking.TotalFare.GetValueOrDefault();

                context.CustomerBookingHistories.Add(customerBookingHistory);
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
        }
    }
}
