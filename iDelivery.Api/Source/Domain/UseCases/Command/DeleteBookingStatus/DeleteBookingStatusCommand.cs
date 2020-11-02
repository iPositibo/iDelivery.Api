using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteBookingStatusCommand : IRequest
    {
        public int BookingStatusId { get; set; }

        public DeleteBookingStatusCommand(int bookingStatusId) => this.BookingStatusId = bookingStatusId;

        private class RequestHandler : IRequestHandler<DeleteBookingStatusCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteBookingStatusCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.BookingStatus.FindAsync(request.BookingStatusId);
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
