using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateRoleCommand : IRequest<CreateRoleDto>
    {
        public CreateRoleDto Dto { get; }

        public CreateRoleCommand(CreateRoleDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateRoleCommand, CreateRoleDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateRoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<Role>(request.Dto);
                context.Roles.Add(entity);
                await context.SaveChangesAsync();

                var result = mapper.Map<CreateRoleDto>(entity);
                return result;
            }
        }
    }
}
