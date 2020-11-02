using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllWalletsQuery : IRequest<List<GetAllWalletsDto>>
    {
        private class GetAllWalletsQueryHandler : IRequestHandler<GetAllWalletsQuery, List<GetAllWalletsDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllWalletsQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllWalletsDto>> Handle(GetAllWalletsQuery request, CancellationToken cancellationToken)
            {
                var wallets = mapper.Map<List<GetAllWalletsDto>>(await context.Wallets.ToListAsync());
                foreach(var wallet in wallets)
                {
                    var rider = await context.Riders.FindAsync(wallet.RiderId);
                    if (rider != null)
                        wallet.RiderName = $"{ rider.LastName }, { rider.FirstName }";

                    var walletStatus = await context.WalletStatus.FindAsync(wallet.WalletStatusId);
                    if (walletStatus != null)
                        wallet.Status = walletStatus.Status;

                    if (wallet.DateUpdated == null)
                        wallet.DateUpdatedFormatted = string.Empty;
                    else
                        wallet.DateUpdatedFormatted = wallet.DateUpdated.GetValueOrDefault().ToString("MM/dd/yyyy");

                    wallet.DateCreatedFormatted = wallet.DateCreated.ToString("MM/dd/yyyy");
                }

                return wallets;
            }
        }
    }
}
