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
    public class PickupBooking : IRequest<PickupBookingDto>
    {
        public int BookingId { get; set; }
        public int RiderId { get; set; }

        public PickupBooking(int bookingId, int RiderId) 
        {
            this.BookingId = bookingId;
            this.RiderId = RiderId;
        }

        private class RequestHandler : IRequestHandler<PickupBooking, PickupBookingDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<PickupBookingDto> Handle(PickupBooking request, CancellationToken cancellationToken)
            {
                var booking = await context.Bookings.FindAsync(request.BookingId);
                if (booking == null)
                    throw new NotFoundException();

                var bookingStatus = await context.BookingStatus.FindAsync(booking.BookingStatusId);
                if (bookingStatus == null)
                    throw new NotFoundException();

                if (bookingStatus.BookingStatusName.ToLower() == "cancelled")
                    throw new CancelledBookingIsNotAllowedException();

                if (bookingStatus.BookingStatusName.ToLower() != "for pickup")
                    throw new OnlyStatusPickupIsAllowedException();

                if (booking.RiderId != request.RiderId)
                    throw new RiderIsNotAllowedException();

                var status = await context.BookingStatus.SingleOrDefaultAsync(o => o.BookingStatusName.ToLower() == "to ship");
                if (status != null)
                    booking.BookingStatusId = status.BookingStatusId;

                booking.PickupTime = DateTime.UtcNow;
                booking.RiderId = request.RiderId;
                context.Update(booking);
                context.SaveChanges();

                await LogHistory(request, booking);

                var result = mapper.Map<PickupBookingDto>(booking);
                if (status != null)
                    result.BookingStatusName = status.BookingStatusName;

                return result;
            }

            private async Task LogHistory(PickupBooking request, Booking booking)
            {
                // log in booking history
                var riderBookingHistory = new RiderBookingHistory();
                var rider = await context.Riders.SingleOrDefaultAsync(o => o.UserId == request.RiderId);
                if (rider != null) riderBookingHistory.RiderId = rider.RiderId;

                riderBookingHistory.BookingStatusId = booking.BookingStatusId;
                var customer = await context.Customers.SingleOrDefaultAsync(o => o.UserId == booking.CustomerId);
                if (customer != null) riderBookingHistory.CustomerName = $"{ customer.LastName }, {customer.FirstName}";
                riderBookingHistory.CustomerNumber = customer.ContactNumber;
                riderBookingHistory.DropOffLocation = booking.DropOffLocation;
                riderBookingHistory.PickupLocation = booking.PickupLocation;
                riderBookingHistory.ItemDetails = booking.Items;
                riderBookingHistory.ReceiverName = booking.ContactName;
                riderBookingHistory.ReceiverNumber = booking.ContactNumber;
                var fare = await context.Fares.FindAsync(booking.FareId);
                if (fare != null)
                {
                    riderBookingHistory.RiderShares = fare.RidersPercentage;
                    riderBookingHistory.TotalKilometers = booking.TotalKilometers;
                }

                riderBookingHistory.TotalFare = booking.TotalFare.GetValueOrDefault();
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
        }
    }
}
