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
    public class CancelRiderBooking : IRequest<CancelBookingDto>
    {
        public int BookingId { get; set; }

        public int RiderId { get; set; }

        public CancelRiderBooking(int bookingId, int riderId)
        {
            this.BookingId = bookingId;
            this.RiderId = riderId;
        }

        private class RequestHandler : IRequestHandler<CancelRiderBooking, CancelBookingDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CancelBookingDto> Handle(CancelRiderBooking request, CancellationToken cancellationToken)
            {
                var booking = await context.Bookings.FindAsync(request.BookingId);
                if (booking == null)
                    throw new NotFoundException();

                // update rider total number of cancelled booking.
                var rider = await context.Riders.SingleOrDefaultAsync(o => o.RiderId == request.RiderId);
                if (rider != null)
                    rider.TotalCancelledBooking += 1;

                await LogHistory(booking);

                return mapper.Map<CancelBookingDto>(booking);
            }

            private async Task LogHistory(Booking booking)
            {
                // log in booking history
                var riderBookingHistory = new RiderBookingHistory();
                if (booking.CustomerId > 0)
                {
                    var customer = await context.Customers.SingleOrDefaultAsync(o => o.UserId == booking.CustomerId);
                    riderBookingHistory.CustomerName = $"{ customer.LastName }, {customer.FirstName}";
                }
                riderBookingHistory.CustomerNumber = booking.ContactNumber;
                riderBookingHistory.ReceiverName = booking.ContactName;
                riderBookingHistory.ReceiverNumber = booking.ContactNumber;
                riderBookingHistory.PickupLocation = booking.PickupLocation;
                riderBookingHistory.DropOffLocation = booking.DropOffLocation;
                riderBookingHistory.RiderId = booking.RiderId.GetValueOrDefault();
                riderBookingHistory.BookingStatusId = booking.BookingStatusId;
                riderBookingHistory.ItemDetails = booking.Items;
                context.RiderBookingHistories.Add(riderBookingHistory);

                var fare = await context.Fares.FindAsync(booking.FareId);
                if (fare != null)
                {
                    riderBookingHistory.RiderShares = fare.RidersPercentage;
                    riderBookingHistory.TotalKilometers = booking.TotalKilometers;
                    riderBookingHistory.TotalFare = booking.TotalFare.GetValueOrDefault();
                }

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
