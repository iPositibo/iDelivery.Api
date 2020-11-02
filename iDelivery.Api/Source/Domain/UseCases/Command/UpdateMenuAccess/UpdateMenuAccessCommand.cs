using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateMenuAccessCommand : IRequest
    {
        public int MenuAccessId { get; set; }
        public UpdateMenuAccessDto Dto { get; }

        public UpdateMenuAccessCommand(int menuAccessId, UpdateMenuAccessDto dto)
        {
            this.MenuAccessId = menuAccessId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateMenuAccessCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateMenuAccessCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<MenuAccess>(request.Dto);

                var menu = await context.MenuAccesses.FindAsync(request.MenuAccessId);
                if (menu == null)
                    throw new NotFoundException();

                // update menu access properties
                menu.MenuItemId = entity.MenuItemId;
                menu.RoleId = entity.RoleId;

                context.Update(menu);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
