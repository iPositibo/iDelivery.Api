using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllAllowedLocationsQuery : IRequest<List<GetAllAllowedLocationsDto>>
    {
        private class GetAllAllowedLocationsQueryHandler : IRequestHandler<GetAllAllowedLocationsQuery, List<GetAllAllowedLocationsDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllAllowedLocationsQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllAllowedLocationsDto>> Handle(GetAllAllowedLocationsQuery request, CancellationToken cancellationToken)
            {
                var result = await context.AllowedLocations.Where(o => o.IsAllowed).ToListAsync();
                if (result == null)
                    throw new NotFoundException();

                var allowedLocations = mapper.Map<List<GetAllAllowedLocationsDto>>(result);

                return allowedLocations;
            }
        }
    }
}
