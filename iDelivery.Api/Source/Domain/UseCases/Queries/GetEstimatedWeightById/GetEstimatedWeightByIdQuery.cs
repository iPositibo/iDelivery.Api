using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetEstimatedWeightByIdQuery : IRequest<GetEstimatedWeightByIdDto>
    {
        public int Id { get; }

        public GetEstimatedWeightByIdQuery(int id) => this.Id = id;

        private class GetEstimatedWeightByIdQueryHandler : IRequestHandler<GetEstimatedWeightByIdQuery, GetEstimatedWeightByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetEstimatedWeightByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetEstimatedWeightByIdDto> Handle(GetEstimatedWeightByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await context.EstimatedWeights.FindAsync(request.Id);
                return mapper.Map<GetEstimatedWeightByIdDto>(user);
            }
        }
    }
}
