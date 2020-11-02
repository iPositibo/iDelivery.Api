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
    public class AcceptBooking : IRequest<AcceptBookingDto>
    { 
        public int BookingId { get; set; }
        public int RiderId { get; set; }

        public AcceptBooking(int bookingId, int riderId)
        {
            this.BookingId = bookingId;
            this.RiderId = riderId;
        }

        private class RequestHandler : IRequestHandler<AcceptBooking, AcceptBookingDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<AcceptBookingDto> Handle(AcceptBooking request, CancellationToken cancellationToken)
            {
                var booking = await context.Bookings.FindAsync(request.BookingId);
                if (booking == null)
                    throw new NotFoundException();

                booking.RiderId = request.RiderId;

                var bookingStatus = await context.BookingStatus.FindAsync(booking.BookingStatusId);
                if (bookingStatus == null)
                    throw new NotFoundException();

                if (bookingStatus.BookingStatusName.ToLower() == "cancelled")
                    throw new CancelledBookingIsNotAllowedException();

                if (bookingStatus.BookingStatusName.ToLower() != "ready")
                    throw new OnlyStatusReadyIsAllowedException();

                var status = await context.BookingStatus.SingleOrDefaultAsync(o => o.BookingStatusName.ToLower() == "for pickup");
                if (status != null)
                    booking.BookingStatusId = status.BookingStatusId;

                context.Update(booking);
                await context.SaveChangesAsync();
                await LogHistory(request, booking);

                var result = mapper.Map<AcceptBookingDto>(booking);
                if (status != null)
                    result.BookingStatusName = status.BookingStatusName;

                return result;
            }

            private async Task LogHistory(AcceptBooking request, Booking booking)
            {
                // log in booking history
                var riderBookingHistory = new RiderBookingHistory();
                riderBookingHistory.RiderId = request.RiderId;

                riderBookingHistory.BookingStatusId = booking.BookingStatusId;
                var customer = await context.Customers.SingleOrDefaultAsync(o => o.UserId == booking.CustomerId);
                if (customer != null) riderBookingHistory.CustomerName = $"{ customer.LastName }, {customer.FirstName}";
                riderBookingHistory.CustomerNumber = customer.ContactNumber;
                riderBookingHistory.DropOffLocation = booking.DropOffLocation;
                riderBookingHistory.ItemDetails = booking.Items;
                riderBookingHistory.ReceiverName = booking.ContactName;
                riderBookingHistory.ReceiverNumber = booking.ContactNumber;
                riderBookingHistory.PickupLocation = booking.PickupLocation;
                var fare = await context.Fares.FindAsync(booking.FareId);
                if (fare != null)
                {
                    riderBookingHistory.RiderShares = fare.RidersPercentage;
                    riderBookingHistory.TotalKilometers = booking.TotalKilometers;
                    riderBookingHistory.TotalFare = booking.TotalFare.GetValueOrDefault();
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
        }
    }
}
