using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetUserByIdQuery : IRequest<GetUserByIdDto>
    {
        public int Id { get; }

        public GetUserByIdQuery(int id) => this.Id = id;

        private class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetUserByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetUserByIdDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await context.Users.FindAsync(request.Id);
                if (result == null)
                    throw new NotFoundException();

                var user = mapper.Map<GetUserByIdDto>(result);

                var userInRole = await context.UserInRoles.SingleOrDefaultAsync(o => o.UserId == user.UserId);
                if (userInRole != null)
                {
                    var role = await context.Roles.FindAsync(userInRole.RoleId);
                    if (role != null)
                        user.RoleName = role.RoleName;
                }

                return user;
            }
        }
    }
}
