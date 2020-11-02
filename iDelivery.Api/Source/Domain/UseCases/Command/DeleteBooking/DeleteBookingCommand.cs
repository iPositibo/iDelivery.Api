using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteBookingCommand : IRequest
    {
        public int BookingId { get; set; }

        public DeleteBookingCommand(int bookingId) => this.BookingId = bookingId;

        private class RequestHandler : IRequestHandler<DeleteBookingCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.Bookings.FindAsync(request.BookingId);
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
