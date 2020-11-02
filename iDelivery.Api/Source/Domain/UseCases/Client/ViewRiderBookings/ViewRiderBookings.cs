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

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ViewRiderBookings : IRequest<List<ViewRiderBookingsDto>>
    {
        public int RiderId { get; }

        public ViewRiderBookings(int riderId) => this.RiderId = riderId;

        private class ViewRiderBookingsHandler : IRequestHandler<ViewRiderBookings, List<ViewRiderBookingsDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public ViewRiderBookingsHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<ViewRiderBookingsDto>> Handle(ViewRiderBookings request, CancellationToken cancellationToken)
            {
                var result = await context.Bookings.Where(o => o.RiderId == request.RiderId).ToListAsync();
                if(result == null)
                    throw new NotFoundException();

                var bookings = mapper.Map<List<ViewRiderBookingsDto>>(result);
                foreach (var booking in bookings)
                {
                    var status = await context.BookingStatus.FindAsync(booking.BookingStatusId);
                    if (status != null)
                        booking.BookingStatusName = status.BookingStatusName;

                    var customer = await context.Customers.SingleOrDefaultAsync(o => o.UserId == booking.CustomerId);
                    if (customer != null)
                        booking.CustomerName = $"{ customer.LastName }, {customer.FirstName}";

                    var rider = await context.Riders.FindAsync(booking.RiderId);
                    if (rider != null)
                        booking.RiderName = $"{ rider.LastName }, {rider.FirstName}";

                    var deduction = booking.TotalFare * 20 / 100;
                    var riderFare = booking.TotalFare - deduction;
                    booking.RiderFare = riderFare;
                    booking.RiderDeduction = deduction;

                    booking.RiderFareFormatted = booking.RiderFare.GetValueOrDefault().ToString("0.00");
                    booking.RiderDeductionFormatted = booking.RiderDeduction.GetValueOrDefault().ToString("0.00");
                    booking.TotalFareFormatted = booking.TotalFare.GetValueOrDefault().ToString("0.00");
                    booking.BookingDateFormatted = booking.BookingDate.ToString("MM/dd/yyyy");
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
