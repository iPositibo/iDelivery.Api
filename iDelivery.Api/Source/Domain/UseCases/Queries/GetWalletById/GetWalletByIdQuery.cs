using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetWalletByIdQuery : IRequest<GetWalletByIdDto>
    {
        public int Id { get; }

        public GetWalletByIdQuery(int id) => this.Id = id;

        private class GetWalletByIdQueryHandler : IRequestHandler<GetWalletByIdQuery, GetWalletByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetWalletByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetWalletByIdDto> Handle(GetWalletByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await context.Wallets.FindAsync(request.Id);
                var wallet = mapper.Map<GetWalletByIdDto>(result);

                var rider = await context.Riders.FindAsync(wallet.RiderId);
                if (rider != null)
                    wallet.RiderName = $"{ rider.LastName }, { rider.FirstName }";

                var walletStatus = await context.WalletStatus.FindAsync(wallet.WalletStatusId);
                if (walletStatus != null)
                    wallet.Status = walletStatus.Status;

                if (wallet.DateUpdated == null)
                    wallet.DateUpdatedFormatted = wallet.DateUpdated.GetValueOrDefault().ToString("MM/dd/yyyy");

                if (wallet.DateCreated == null)
                    wallet.DateCreatedFormatted = wallet.DateCreated.ToString("MM/dd/yyyy");

                return wallet;
            }
        }
    }
}
