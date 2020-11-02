using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteMenuItemCommand : IRequest
    {
        public int MenuId { get; set; }

        public DeleteMenuItemCommand(int menuId) => this.MenuId = menuId;

        private class RequestHandler : IRequestHandler<DeleteMenuItemCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.MenuItems.FindAsync(request.MenuId);
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
