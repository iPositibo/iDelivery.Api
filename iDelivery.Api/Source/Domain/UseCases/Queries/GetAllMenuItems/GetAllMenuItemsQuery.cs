using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllMenuItemsQuery : IRequest<List<GetAllMenuItemsDto>>
    {
        private class GetAllMenuItemsQueryHandler : IRequestHandler<GetAllMenuItemsQuery, List<GetAllMenuItemsDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllMenuItemsQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllMenuItemsDto>> Handle(GetAllMenuItemsQuery request, CancellationToken cancellationToken)
            {
                var result = mapper.Map<List<GetAllMenuItemsDto>>(await context.MenuItems.ToListAsync());
                return result;
            }
        }
    }
}
