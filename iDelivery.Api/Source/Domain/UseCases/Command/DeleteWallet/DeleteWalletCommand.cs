using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteWalletCommand : IRequest
    {
        public int WalletId { get; set; }

        public DeleteWalletCommand(int walletId) => this.WalletId = walletId;

        private class RequestHandler : IRequestHandler<DeleteWalletCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteWalletCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.Wallets.FindAsync(request.WalletId);
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
