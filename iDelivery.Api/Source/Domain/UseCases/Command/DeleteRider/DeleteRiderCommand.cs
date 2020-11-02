using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteRiderCommand : IRequest
    {
        public int RiderId { get; set; }

        public DeleteRiderCommand(int riderId) => this.RiderId = riderId;

        private class RequestHandler : IRequestHandler<DeleteRiderCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteRiderCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.Riders.FindAsync(request.RiderId);
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
