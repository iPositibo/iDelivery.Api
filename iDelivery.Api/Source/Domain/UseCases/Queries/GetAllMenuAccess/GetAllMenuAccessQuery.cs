using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllMenuAccessQuery : IRequest<List<GetAllMenuAccessDto>>
    {
        private class GetAllMenuItemsQueryHandler : IRequestHandler<GetAllMenuAccessQuery, List<GetAllMenuAccessDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllMenuItemsQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllMenuAccessDto>> Handle(GetAllMenuAccessQuery request, CancellationToken cancellationToken)
            {
                var result = await context.MenuAccesses.ToListAsync();
                if (result == null)
                    throw new NotFoundException();

                var menus = mapper.Map<List<GetAllMenuAccessDto>>(result);
                foreach(var menu in menus)
                {
                    menu.DateCreatedFormatted = menu.DateCreated.ToString("MM/dd/yyyy");
                    if(menu.DateUpdated != null)
                        menu.DateUpdatedFormatted = menu.DateUpdated.GetValueOrDefault().ToString("MM/dd/yyyy");

                    var role = await context.Roles.FindAsync(menu.RoleId);
                    if (role != null)
                        menu.RoleName = role.RoleName;

                    var menuItem = await context.MenuItems.FindAsync(menu.MenuItemId);
                    if (menuItem != null)
                        menu.MenuItemName = menuItem.Text;
                }

                return menus;
            }
        }
    }
}
