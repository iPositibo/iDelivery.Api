using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteBlockedRiderCommand : IRequest
    {
        public int RiderId { get; set; }

        public DeleteBlockedRiderCommand(int riderId) => this.RiderId = riderId;

        private class RequestHandler : IRequestHandler<DeleteBlockedRiderCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteBlockedRiderCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.BlockedRiders.FindAsync(request.RiderId);
                if (entity == null)
                {
                    throw new NotFoundException();
                }

                context.Remove(entity);
                await context.SaveChangesAsync();

                // set the rider status to active again.
                var rider = await context.Riders.FindAsync(request.RiderId);
                if (rider != null)
                {
                    var riderStatus = await context.RiderStatus.SingleOrDefaultAsync(o => o.Status.ToLower() == "active");
                    if (riderStatus != null)
                        rider.RiderStatusId = riderStatus.RiderStatusId;

                    context.Riders.Update(rider);
                    await context.SaveChangesAsync();
                }

                return Unit.Value;
            }
        }
    }
}
