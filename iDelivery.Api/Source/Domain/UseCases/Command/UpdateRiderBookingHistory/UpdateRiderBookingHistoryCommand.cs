using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateRiderBookingHistoryCommand : IRequest
    {
        public int RiderBookingHistoryId { get; set; }
        public UpdateRiderBookingHistoryDto Dto { get; }

        public UpdateRiderBookingHistoryCommand(int riderBookingHistoryId, UpdateRiderBookingHistoryDto dto)
        {
            this.RiderBookingHistoryId = riderBookingHistoryId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateRiderBookingHistoryCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateRiderBookingHistoryCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<RiderBookingHistory>(request.Dto);

                var bookingHistory = await context.RiderBookingHistories.FindAsync(request.RiderBookingHistoryId);
                if (bookingHistory == null)
                    throw new NotFoundException();

                // update rider booking history properties
                bookingHistory.BookingStatusId = entity.BookingStatusId;
                bookingHistory.RiderId = entity.RiderId;
                bookingHistory.TotalFare = entity.TotalFare;
                bookingHistory.TotalKilometers = entity.TotalKilometers;
                bookingHistory.CustomerName = entity.CustomerName;
                bookingHistory.CustomerNumber = entity.CustomerNumber;
                bookingHistory.DropOffLocation = entity.DropOffLocation;
                bookingHistory.ItemDetails = entity.ItemDetails;
                bookingHistory.PickupLocation = entity.PickupLocation;
                bookingHistory.ReceiverName = entity.ReceiverName;
                bookingHistory.ReceiverNumber = entity.ReceiverNumber;
                bookingHistory.RiderShares = entity.RiderShares;

                context.Update(bookingHistory);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
