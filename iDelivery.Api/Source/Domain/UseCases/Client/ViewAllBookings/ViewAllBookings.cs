using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using iDelivery.Api.Source.Domain.UseCases.Queries;
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
    public class ViewAllBookings : IRequest<List<ViewAllBookingsDto>>
    {
        private class ViewAllBookingsHandler : IRequestHandler<ViewAllBookings, List<ViewAllBookingsDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public ViewAllBookingsHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<ViewAllBookingsDto>> Handle(ViewAllBookings request, CancellationToken cancellationToken)
            {
                // filter by status ready. only ready status will be retrieved.
                var bookingStatus = await context.BookingStatus.SingleOrDefaultAsync(o => o.BookingStatusName.ToLower() == "ready");
                if (bookingStatus == null)
                    throw new NotFoundException();

                var result = await context.Bookings.Where(o => o.BookingStatusId == bookingStatus.BookingStatusId).ToListAsync();
                if (result == null)
                    throw new BookingsWithStatusReadyNotFoundException();

                var bookings = mapper.Map<List<ViewAllBookingsDto>>(result);
                foreach (var booking in bookings)
                {
                    var status = await context.BookingStatus.FindAsync(booking.BookingStatusId);
                    if (status != null)
                        booking.BookingStatusName = status.BookingStatusName;

                    var customer = await context.Customers.SingleOrDefaultAsync(o => o.UserId == booking.CustomerId);
                    if (customer != null)
                    {
                        booking.CustomerName = $"{ customer.LastName }, {customer.FirstName}";
                        booking.CustomerNumber = customer.ContactNumber;
                        booking.Customer = mapper.Map<GetCustomerByIdDto>(customer);
                    }

                    var rider = await context.Riders.FindAsync(booking.RiderId);
                    if (rider != null)
                        booking.RiderName = $"{ rider.LastName }, {rider.FirstName}";

                    if (booking.FareId != null)
                    {
                        var fare = await context.Fares.FindAsync(booking.FareId);
                        if (fare != null)
                        {
                            booking.Fare = mapper.Map<GetFareByIdDto>(fare);
                            //booking.TotalFare = FareHelper.Compute(fare.PricePerKilometer, fare.BaseFare, fare.Surcharge, GetKilometer(booking.TotalKilometers));
                        }
                    }
                 
                    var deduction = booking.TotalFare * 20 / 100;
                    var riderFare = booking.TotalFare - deduction;
                    booking.RiderFare = riderFare;
                    booking.RiderDeduction = deduction;

                    booking.RiderFareFormatted = booking.RiderFare.GetValueOrDefault().ToString("0.00");
                    booking.RiderDeductionFormatted = booking.RiderDeduction.GetValueOrDefault().ToString("0.00");
                    booking.TotalFareFormatted = booking.TotalFare.GetValueOrDefault().ToString("0.00");
                    booking.BookingDateFormatted = booking.BookingDate.ToString("MM/dd/yyyy");
                   
                    booking.ReceiverName = booking.ContactName;
                    booking.ReceiverNumber = booking.ContactNumber;
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
