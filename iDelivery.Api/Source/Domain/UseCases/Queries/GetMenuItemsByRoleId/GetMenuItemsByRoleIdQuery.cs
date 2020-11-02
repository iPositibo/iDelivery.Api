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
    public class GetMenuItemsByRoleIdQuery : IRequest<List<GetMenuItemsByRoleIdDto>>
    {
        public int RoleId { get; }

        public GetMenuItemsByRoleIdQuery(int roleId) => this.RoleId = roleId;

        private class GetAllMenuItemsQueryHandler : IRequestHandler<GetMenuItemsByRoleIdQuery, List<GetMenuItemsByRoleIdDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllMenuItemsQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetMenuItemsByRoleIdDto>> Handle(GetMenuItemsByRoleIdQuery request, CancellationToken cancellationToken)
            {
                var result = await context.MenuAccesses.Where(o => o.RoleId == request.RoleId).ToListAsync();
                if (result == null)
                    throw new NotFoundException();

                var menus = mapper.Map<List<GetMenuAccessByRoleIdDto>>(result);
                var menuItems = new List<GetMenuItemsByRoleIdDto>();
                foreach (var menu in menus)
                {
                    var item = await context.MenuItems.FindAsync(menu.MenuItemId);
                    if (item == null)
                        continue;

                    menuItems.Add(new GetMenuItemsByRoleIdDto
                    {
                        MenuItemId = item.MenuItemId,
                        Icon = item.Icon,
                        Text = item.Text,
                        Link = item.Link
                    });
                }

                return menuItems.OrderBy(o => o.Text).ToList();
            }
        }
    }
}
