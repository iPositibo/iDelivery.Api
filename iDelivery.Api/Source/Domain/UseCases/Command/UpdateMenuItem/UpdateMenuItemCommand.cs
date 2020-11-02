using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateMenuItemCommand : IRequest
    {
        public int MenuItemId { get; set; }
        public UpdateMenuItemDto Dto { get; }

        public UpdateMenuItemCommand(int menuItemId, UpdateMenuItemDto dto)
        {
            this.MenuItemId = menuItemId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateMenuItemCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<MenuItem>(request.Dto);

                var menu = await context.MenuItems.FindAsync(request.MenuItemId);
                if (menu == null)
                    throw new NotFoundException();

                // update menu item properties
                menu.Icon = entity.Icon;
                menu.Text = entity.Text;
                menu.Link = entity.Link;

                context.Update(menu);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
