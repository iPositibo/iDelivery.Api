using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateWalletCommand : IRequest
    {
        public int WalletId { get; set; }
        public UpdateWalletDto Dto { get; }

        public UpdateWalletCommand(int walletId, UpdateWalletDto dto)
        {
            this.WalletId = walletId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateWalletCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<Wallet>(request.Dto);

                var wallet = await context.Wallets.FindAsync(request.WalletId);
                if (wallet == null)
                    throw new NotFoundException();

                // update wallet properties
                wallet.RiderId = entity.RiderId;
                wallet.CurrentPoints += entity.PointsLoaded;
                wallet.DateCreated = entity.DateCreated;
                wallet.DateUpdated = DateTime.UtcNow;
                wallet.PointsLoaded = entity.PointsLoaded;
                wallet.WalletStatusId = entity.WalletStatusId;
                wallet.NegativeBalance = entity.NegativeBalance;

                context.Update(wallet);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
