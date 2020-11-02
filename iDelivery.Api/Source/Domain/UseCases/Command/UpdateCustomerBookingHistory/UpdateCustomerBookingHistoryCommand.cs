using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateCustomerBookingHistoryCommand : IRequest
    {
        public int CustomerBookingHistoryId { get; set; }
        public UpdateCustomerBookingHistoryDto Dto { get; }

        public UpdateCustomerBookingHistoryCommand(int customerBookingHistoryId, UpdateCustomerBookingHistoryDto dto)
        {
            this.CustomerBookingHistoryId = customerBookingHistoryId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateCustomerBookingHistoryCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateCustomerBookingHistoryCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<CustomerBookingHistory>(request.Dto);

                var bookingHistory = await context.CustomerBookingHistories.FindAsync(request.CustomerBookingHistoryId);
                if (bookingHistory == null)
                    throw new NotFoundException();

                // update customer booking history properties
                bookingHistory.BookingStatusId = entity.BookingStatusId;
                bookingHistory.CustomerId = entity.CustomerId;
                bookingHistory.EstimatedTime = entity.EstimatedTime;
                bookingHistory.ItemDetails = entity.ItemDetails;
                bookingHistory.Receipt = entity.Receipt;
                bookingHistory.ReceiverCompleteName = entity.ReceiverCompleteName;
                bookingHistory.ReceiverCompleteAddress = entity.ReceiverCompleteAddress;
                bookingHistory.TotalFare = entity.TotalFare;
                bookingHistory.TotalKilometers = entity.TotalKilometers;

                context.Update(bookingHistory);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
