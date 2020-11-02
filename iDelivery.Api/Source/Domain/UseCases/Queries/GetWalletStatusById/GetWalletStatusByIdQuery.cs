using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetWalletStatusByIdQuery : IRequest<GetWalletStatusByIdDto>
    {
        public int Id { get; }

        public GetWalletStatusByIdQuery(int id) => this.Id = id;

        private class GetWalletStatusByIdQueryHandler : IRequestHandler<GetWalletStatusByIdQuery, GetWalletStatusByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetWalletStatusByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetWalletStatusByIdDto> Handle(GetWalletStatusByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await context.WalletStatus.FindAsync(request.Id);
                var wallet = mapper.Map<GetWalletStatusByIdDto>(result);

                return wallet;
            }
        }
    }
}
