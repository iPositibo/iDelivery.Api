using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteRoleCommand : IRequest
    {
        public int RoleId { get; set; }

        public DeleteRoleCommand(int roleId) => this.RoleId = roleId;

        private class RequestHandler : IRequestHandler<DeleteRoleCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.Roles.FindAsync(request.RoleId);
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
