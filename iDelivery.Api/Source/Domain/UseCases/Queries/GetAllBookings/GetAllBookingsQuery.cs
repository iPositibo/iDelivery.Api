using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Infrastructure.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllBookingsQuery : IRequest<List<GetAllBookingsDto>>
    {
        private class GetAllBookingsQueryHandler : IRequestHandler<GetAllBookingsQuery, List<GetAllBookingsDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllBookingsQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllBookingsDto>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
            {
                var bookings = mapper.Map<List<GetAllBookingsDto>>(await context.Bookings.ToListAsync());
                foreach(var booking in bookings)
                {
                    var status = await context.BookingStatus.FindAsync(booking.BookingStatusId);
                    if (status != null)
                    {
                        booking.BookingStatusName = status.BookingStatusName;
                        booking.BookingStatusColor = status.StatusColor;
                    }

                    var customer = await context.Customers.SingleOrDefaultAsync(o => o.UserId == booking.CustomerId);
                    if (customer != null)
                        booking.CustomerName = $"{ customer.LastName }, {customer.FirstName}";

                    var rider = await context.Riders.FindAsync(booking.RiderId);
                    if (rider != null)
                        booking.RiderName = $"{ rider.LastName }, {rider.FirstName}";

                    booking.BookingDateFormatted = booking.BookingDate.ToString("MM/dd/yyyy");
                    booking.PickupTimeFormatted = booking.PickupTime.GetValueOrDefault().ToString("hh:mm tt");
                    booking.DropOffTimeFormatted = booking.DropOffTime.GetValueOrDefault().ToString("hh:mm tt");

                    var fare = await context.Fares.FindAsync(booking.FareId);
                    if (fare != null)
                        booking.TotalFare = FareHelper.Compute(fare.PricePerKilometer, fare.BaseFare, fare.Surcharge, GetKilometer(booking.TotalKilometers));
                }

                return bookings;
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
