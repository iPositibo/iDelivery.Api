using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteWalletStatusCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteWalletStatusCommand(int id) => this.Id = id;

        private class RequestHandler : IRequestHandler<DeleteWalletStatusCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteWalletStatusCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.WalletStatus.FindAsync(request.Id);
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
