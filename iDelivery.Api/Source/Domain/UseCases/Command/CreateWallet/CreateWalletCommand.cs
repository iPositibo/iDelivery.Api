using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateWalletCommand : IRequest<CreateWalletDto>
    {
        public CreateWalletDto Dto { get; }

        public CreateWalletCommand(CreateWalletDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateWalletCommand, CreateWalletDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateWalletDto> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<Wallet>(request.Dto);
                var wallet = await context.Wallets.SingleOrDefaultAsync(o => o.RiderId == entity.RiderId);
                if (wallet != null)
                    throw new RiderWalletAlreadyExist();

                // log wallet transaction
                var walletLog = new WalletLog();
                walletLog.RiderId = entity.RiderId;
                walletLog.LogDate = DateTime.UtcNow;
                walletLog.Points = entity.PointsLoaded;
                walletLog.CurrentPoints = entity.CurrentPoints + entity.PointsLoaded;

                entity.CurrentPoints = entity.CurrentPoints + entity.PointsLoaded;
                entity.DateCreated = DateTime.UtcNow;

                context.Wallets.Add(entity);
                await context.SaveChangesAsync();

                if (entity.WalletStatusId > 0)
                {
                    var walletStatus = await context.WalletStatus.FindAsync(entity.WalletStatusId);
                    if(walletStatus != null)
                        walletLog.CurrentStatus = walletStatus.Status;
                }

                context.WalletLogs.Add(walletLog);
                await context.SaveChangesAsync();

                var result = mapper.Map<CreateWalletDto>(entity);
                return result;
            }
        }
    }
}
