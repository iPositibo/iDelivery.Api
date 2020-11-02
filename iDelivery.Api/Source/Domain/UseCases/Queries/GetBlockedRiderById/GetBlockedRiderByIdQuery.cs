using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetBlockedRiderByIdQuery : IRequest<GetBlockedRiderByIdDto>
    {
        public int Id { get; }

        public GetBlockedRiderByIdQuery(int id) => this.Id = id;

        private class GetBlockedRiderByIdQueryHandler : IRequestHandler<GetBlockedRiderByIdQuery, GetBlockedRiderByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetBlockedRiderByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetBlockedRiderByIdDto> Handle(GetBlockedRiderByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await context.BlockedRiders.FindAsync(request.Id);
                var blockedRider = mapper.Map<GetBlockedRiderByIdDto>(result);

                var rider = await context.Customers.FindAsync(blockedRider.RiderId);
                if (rider != null)
                    blockedRider.RiderName = $"{ rider.LastName }, {rider.FirstName}";

                blockedRider.DateBlockedFormatted = blockedRider.DateBlocked.ToString("MM/dd/yyyy");

                return blockedRider;
            }
        }
    }
}
