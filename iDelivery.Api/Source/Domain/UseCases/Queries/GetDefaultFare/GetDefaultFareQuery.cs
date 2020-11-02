using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetDefaultFareQuery : IRequest<GetDefaultFareDto>
    {
        private class GetDefaultFareQueryHandler : IRequestHandler<GetDefaultFareQuery, GetDefaultFareDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetDefaultFareQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetDefaultFareDto> Handle(GetDefaultFareQuery request, CancellationToken cancellationToken)
            {
                var user = await context.Fares.FirstOrDefaultAsync(o => o.IsDefault == true);
                return mapper.Map<GetDefaultFareDto>(user);
            }
        }
    }
}
