using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using iDelivery.Api.Source.Infrastructure.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllRiderTransactionsQuery : IRequest<List<GetAllRiderTransactionsDto>>
    {
        private class GetAllRiderTransactionsQueryHandler : IRequestHandler<GetAllRiderTransactionsQuery, List<GetAllRiderTransactionsDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllRiderTransactionsQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllRiderTransactionsDto>> Handle(GetAllRiderTransactionsQuery request, CancellationToken cancellationToken)
            {
                // filter by status ready. only ready status will be retrieved.
                var bookingStatus = await context.BookingStatus.SingleOrDefaultAsync(o => o.BookingStatusName.ToLower() == "ready");
                if (bookingStatus == null)
                    throw new NotFoundException();

                var result = await context.Bookings.Where(o => o.BookingStatusId == bookingStatus.BookingStatusId).ToListAsync();
                if (result == null)
                    throw new BookingsWithStatusReadyNotFoundException();

                var bookings = mapper.Map<List<GetAllRiderTransactionsDto>>(result);
                foreach (var booking in bookings)
                {
                    var status = await context.BookingStatus.FindAsync(booking.BookingStatusId);
                    if (status != null)
                        booking.BookingStatusName = status.BookingStatusName;

                    var customer = await context.Customers.FindAsync(booking.CustomerId);
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
