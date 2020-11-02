using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteAllowedLocationCommand : IRequest
    {
        public int AllowedLocationId { get; set; }

        public DeleteAllowedLocationCommand(int allowedLocationId) => this.AllowedLocationId = allowedLocationId;

        private class RequestHandler : IRequestHandler<DeleteAllowedLocationCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteAllowedLocationCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.AllowedLocations.FindAsync(request.AllowedLocationId);
                if (entity == null)
                    throw new NotFoundException();

                context.Remove(entity);
                context.SaveChanges();

                return Unit.Value;
            }
        }
    }
}
