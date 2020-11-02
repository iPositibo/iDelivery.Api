using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteBookingHistoryCommand : IRequest
    {
        public int BookingHistoryId { get; set; }

        public DeleteBookingHistoryCommand(int bookingHistoryId) => this.BookingHistoryId = bookingHistoryId;

        private class RequestHandler : IRequestHandler<DeleteBookingHistoryCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteBookingHistoryCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.BookingHistories.FindAsync(request.BookingHistoryId);
                if (entity == null)
                {
                    throw new NotFoundException();
                }

                context.Remove(entity);
                context.SaveChanges();

                return Unit.Value;
            }
        }
    }
}
