using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateBookingHistoryCommand : IRequest
    {
        public int BookingHistoryId { get; set; }
        public UpdateBookingHistoryDto Dto { get; }

        public UpdateBookingHistoryCommand(int bookingHistoryId, UpdateBookingHistoryDto dto)
        {
            this.BookingHistoryId = bookingHistoryId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateBookingHistoryCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateBookingHistoryCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<BookingHistory>(request.Dto);

                var bookingHistory = await context.BookingHistories.FindAsync(request.BookingHistoryId);
                if (bookingHistory == null)
                    throw new NotFoundException();

                // update booking history properties
                bookingHistory.BookingDate = entity.BookingDate;
                bookingHistory.BookingStatus = entity.BookingStatus;
                bookingHistory.CustomerId = entity.CustomerId;

                context.Update(bookingHistory);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
