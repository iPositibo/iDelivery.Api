using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteRiderBookingHistoryCommand : IRequest
    {
        public int RiderBookingHistoryId { get; set; }

        public DeleteRiderBookingHistoryCommand(int riderBookingHistoryId) => this.RiderBookingHistoryId = riderBookingHistoryId;

        private class RequestHandler : IRequestHandler<DeleteRiderBookingHistoryCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteRiderBookingHistoryCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.RiderBookingHistories.FindAsync(request.RiderBookingHistoryId);
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
