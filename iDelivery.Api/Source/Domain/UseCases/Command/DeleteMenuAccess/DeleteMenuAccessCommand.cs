using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteMenuAccessCommand : IRequest
    {
        public int MenuAccessId { get; set; }

        public DeleteMenuAccessCommand(int menuAccessId) => this.MenuAccessId = menuAccessId;

        private class RequestHandler : IRequestHandler<DeleteMenuAccessCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteMenuAccessCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.MenuAccesses.FindAsync(request.MenuAccessId);
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
