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
    public class GetAllUsersQuery : IRequest<List<GetAllUsersDto>>
    {
        private class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<GetAllUsersDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllUsersQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllUsersDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {
                var result = await context.Users.ToListAsync();
                if (result == null)
                    throw new NotFoundException();

                var users = mapper.Map<List<GetAllUsersDto>>(result);
                foreach(var user in users)
                {
                    var userInRole = await context.UserInRoles.SingleOrDefaultAsync(o => o.UserId == user.UserId);
                    if (userInRole == null)
                        continue;

                    var role = await context.Roles.FindAsync(userInRole.RoleId);
                    if (role == null)
                        continue;

                    user.RoleId = role.RoleId;
                    user.RoleName = role.RoleName;
                }

                return users.OrderByDescending(o => o.UserId).ToList();
            }
        }
    }
}
