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
    public class GetAllAvailableRiderUsersQuery : IRequest<List<GetAllAvailableRiderUsersDto>>
    {
        private class GetAllAvailableUsersQueryHandler : IRequestHandler<GetAllAvailableRiderUsersQuery, List<GetAllAvailableRiderUsersDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllAvailableUsersQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllAvailableRiderUsersDto>> Handle(GetAllAvailableRiderUsersQuery request, CancellationToken cancellationToken)
            {
                var result = await context.Users.ToListAsync();
                if (result == null)
                    throw new NotFoundException();

                var users = mapper.Map<List<GetAllAvailableRiderUsersDto>>(result);
                var userList = new List<GetAllAvailableRiderUsersDto>();
                foreach (var user in users)
                {
                    if (await context.Riders.AnyAsync(o => o.UserId == user.UserId))
                        continue;

                    var userInRole = await context.UserInRoles.SingleOrDefaultAsync(o => o.UserId == user.UserId);
                    if (userInRole == null)
                        continue;

                    var role = await context.Roles.FindAsync(userInRole.RoleId);
                    if (role == null)
                        continue;

                    user.RoleName = role.RoleName;

                    if (user.RoleName.ToLower() == "rider")
                        userList.Add(user);
                }

                return userList.OrderByDescending(o => o.UserId).ToList();
            }
        }
    }
}
