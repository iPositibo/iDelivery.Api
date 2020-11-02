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
    public class GetMenuAccessByRoleIdQuery : IRequest<List<GetMenuAccessByRoleIdDto>>
    {
        public int RoleId { get; }

        public GetMenuAccessByRoleIdQuery(int roleId) => this.RoleId = roleId;

        private class GetAllMenuItemsQueryHandler : IRequestHandler<GetMenuAccessByRoleIdQuery, List<GetMenuAccessByRoleIdDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllMenuItemsQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetMenuAccessByRoleIdDto>> Handle(GetMenuAccessByRoleIdQuery request, CancellationToken cancellationToken)
            {
                var result = await context.MenuAccesses.Where(o => o.RoleId == request.RoleId).ToListAsync();
                if (result == null)
                    throw new NotFoundException();

                var menus = mapper.Map<List<GetMenuAccessByRoleIdDto>>(result);
                foreach(var menu in menus)
                {
                    menu.DateCreatedFormatted = menu.DateCreated.ToString("MM/dd/yyyy");
                    if(menu.DateUpdated != null)
                        menu.DateUpdatedFormatted = menu.DateUpdated.GetValueOrDefault().ToString("MM/dd/yyyy");
                }

                return menus;
            }
        }
    }
}
