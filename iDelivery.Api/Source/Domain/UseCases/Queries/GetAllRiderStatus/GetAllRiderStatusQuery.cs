using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllRiderStatusQuery : IRequest<List<GetAllRiderStatusDto>>
    {
        private class GetAllRiderStatusQueryHandler : IRequestHandler<GetAllRiderStatusQuery, List<GetAllRiderStatusDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllRiderStatusQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllRiderStatusDto>> Handle(GetAllRiderStatusQuery request, CancellationToken cancellationToken)
            {
                var result = mapper.Map<List<GetAllRiderStatusDto>>(await context.RiderStatus.ToListAsync());
                return result;
            }
        }
    }
}
