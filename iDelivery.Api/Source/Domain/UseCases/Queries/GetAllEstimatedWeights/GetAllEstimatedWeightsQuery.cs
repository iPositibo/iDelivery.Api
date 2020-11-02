using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllEstimatedWeightsQuery : IRequest<List<GetAllEstimatedWeightsDto>>
    {
        private class GetAllEstimatedWeightsQueryHandler : IRequestHandler<GetAllEstimatedWeightsQuery, List<GetAllEstimatedWeightsDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllEstimatedWeightsQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllEstimatedWeightsDto>> Handle(GetAllEstimatedWeightsQuery request, CancellationToken cancellationToken)
            {
                return  mapper.Map<List<GetAllEstimatedWeightsDto>>(await context.EstimatedWeights.ToListAsync());
            }
        }
    }
}
