using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteWalletLogCommand : IRequest
    {
        public int WalletLogId { get; set; }

        public DeleteWalletLogCommand(int walletLogId) => this.WalletLogId = walletLogId;

        private class RequestHandler : IRequestHandler<DeleteWalletLogCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteWalletLogCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.WalletLogs.FindAsync(request.WalletLogId);
                if (entity == null)
                    throw new NotFoundException();

                context.Remove(entity);
                context.SaveChanges();

                return Unit.Value;
            }
        }
    }
}
