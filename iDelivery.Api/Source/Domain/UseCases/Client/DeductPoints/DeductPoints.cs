using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class DeductPoints : IRequest
    {
        public int WalletId { get; set; }

        public int PointsLoaded { get; set; }

        public DeductPoints(int walletId, int points)
        {
            this.WalletId = walletId;
            this.PointsLoaded = points;
        }

        private class RequestHandler : IRequestHandler<DeductPoints>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(DeductPoints request, CancellationToken cancellationToken)
            {
                var wallet = await context.Wallets.FindAsync(request.WalletId);
                if (wallet == null)
                    throw new NotFoundException();

                wallet.PointsLoaded -= request.PointsLoaded;

                context.Update(wallet);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
