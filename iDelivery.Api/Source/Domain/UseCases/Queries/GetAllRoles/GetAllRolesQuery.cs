using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllRolesQuery : IRequest<List<GetAllRolesDto>>
    {
        private class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, List<GetAllRolesDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllRolesQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllRolesDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
            {
                var result = mapper.Map<List<GetAllRolesDto>>(await context.Roles.ToListAsync());
                return result;
            }
        }
    }
}
