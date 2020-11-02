using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ViewAllowedLocations : IRequest<List<ViewAllowedLocationsDto>>
    {
        private class GetAllAllowedLocationsQueryHandler : IRequestHandler<ViewAllowedLocations, List<ViewAllowedLocationsDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllAllowedLocationsQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<ViewAllowedLocationsDto>> Handle(ViewAllowedLocations request, CancellationToken cancellationToken)
            {
                var result = await context.AllowedLocations.Where(o => o.IsAllowed).ToListAsync();
                if (result == null)
                    throw new NotFoundException();

                return mapper.Map<List<ViewAllowedLocationsDto>>(result);
            }
        }
    }
}
