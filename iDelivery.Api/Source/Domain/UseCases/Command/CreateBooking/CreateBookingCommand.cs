using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Infrastructure.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateBookingCommand : IRequest<CreateBookingDto>
    {
        public CreateBookingDto Dto { get; }

        public CreateBookingCommand(CreateBookingDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateBookingCommand, CreateBookingDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateBookingDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
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

                return mapper.Map<CreateBookingDto>(entity);
            }

            private async Task LogHistory(Booking entity)
            {
                // log in booking history
                var customerBookingHistory = new CustomerBookingHistory();
                customerBookingHistory.CustomerId = entity.CustomerId;
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
