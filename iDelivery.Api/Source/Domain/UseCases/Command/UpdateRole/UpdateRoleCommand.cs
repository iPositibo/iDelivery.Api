using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateRoleCommand : IRequest
    {
        public int UserId { get; set; }
        public UpdateRoleDto Dto { get; }

        public UpdateRoleCommand(int userId, UpdateRoleDto dto)
        {
            this.UserId = userId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateRoleCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<Role>(request.Dto);

                var role = await context.Roles.FindAsync(request.UserId);
                if (role == null)
                    throw new NotFoundException();

                // update role properties
                role.RoleName = entity.RoleName;

                context.Update(role);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
