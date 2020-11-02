using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetWalletLogByIdQuery : IRequest<GetWalletLogByIdDto>
    {
        public int Id { get; }

        public GetWalletLogByIdQuery(int id) => this.Id = id;

        private class GetWalletLogByIdQueryHandler : IRequestHandler<GetWalletLogByIdQuery, GetWalletLogByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetWalletLogByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetWalletLogByIdDto> Handle(GetWalletLogByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await context.WalletLogs.FindAsync(request.Id);
                var log = mapper.Map<GetWalletLogByIdDto>(result);

                log.LogDateFormatted = log.LogDate.ToString("MM/dd/yyyy");
                var rider = await context.Riders.FindAsync(log.RiderId);
                if (rider != null)
                    log.RiderName = $"{ rider.LastName }, { rider.FirstName }";

                return log;
            }
        }
    }
}
