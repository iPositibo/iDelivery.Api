using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using iDelivery.Api.Source.Domain.UseCases.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    /// <summary>
    /// Class for the book delivery.
    /// </summary>
    public class BookDelivery : IRequest<BookDeliveryResult>
    {
        public BookDeliveryDto Dto { get; }

        public BookDelivery(BookDeliveryDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<BookDelivery, BookDeliveryResult>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<BookDeliveryResult> Handle(BookDelivery request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<Booking>(request.Dto);
                if (entity != null)
                    entity.BookingDate = DateTime.UtcNow;

                var fare = await context.Fares.FirstOrDefaultAsync(o => o.IsDefault == true);
                if (fare != null)
                    entity.FareId = fare.FareId;

                entity.IsActive = true;
                entity.ReferenceNumber = Guid.NewGuid().ToString("N");
                context.Bookings.Add(entity);
                await context.SaveChangesAsync();
                
                await LogHistory(entity);

                var booking = mapper.Map<BookDeliveryResult>(entity);
                var customer = await context.Customers.SingleOrDefaultAsync(o => o.UserId == entity.CustomerId);
                if (customer != null)
                {
                    booking.Customer = mapper.Map<GetCustomerByIdDto>(customer);
                    booking.CustomerName = $"{ customer.LastName }, {customer.FirstName}";
                    booking.CustomerNumber = customer.ContactNumber;
                }

                if (booking.FareId != null)
                {
                    var fares = await context.Fares.FindAsync(booking.FareId);
                    if (fares != null)
                        booking.Fare = mapper.Map<GetFareByIdDto>(fares);
                }

                booking.TotalFare = entity.TotalFare.GetValueOrDefault();
                booking.ReceiverName = entity.ContactName;
                booking.ReceiverNumber = entity.ContactNumber;

                return booking;
            }

            private async Task LogHistory(Booking entity)
            {
                var customer = await context.Customers.SingleOrDefaultAsync(o => o.UserId == entity.CustomerId);
                if (customer == null)
                    throw new NotFoundException();

                // log in booking history
                var customerBookingHistory = new CustomerBookingHistory();
                customerBookingHistory.CustomerId = customer.CustomerId;
                customerBookingHistory.BookingStatusId = entity.BookingStatusId;
                customerBookingHistory.ReceiverCompleteName = entity.ContactName;
                customerBookingHistory.ReceiverCompleteAddress = entity.DropOffLocation;
                customerBookingHistory.EstimatedTime = entity.EstimatedTime;
                customerBookingHistory.ItemDetails = entity.Items;
                customerBookingHistory.TotalKilometers = entity.TotalKilometers;
                customerBookingHistory.Receipt = string.Empty;
                customerBookingHistory.BookingDate = entity.BookingDate;
                customerBookingHistory.TotalFare = entity.TotalFare.GetValueOrDefault();

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
