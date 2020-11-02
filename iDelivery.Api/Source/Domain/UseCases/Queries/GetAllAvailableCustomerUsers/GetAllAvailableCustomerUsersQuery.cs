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
    public class GetAllAvailableCustomerUsersQuery : IRequest<List<GetAllAvailableCustomerUsersDto>>
    {
        private class GetAllAvailableCustomerUsersQueryHandler : IRequestHandler<GetAllAvailableCustomerUsersQuery, List<GetAllAvailableCustomerUsersDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllAvailableCustomerUsersQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllAvailableCustomerUsersDto>> Handle(GetAllAvailableCustomerUsersQuery request, CancellationToken cancellationToken)
            {
                var result = await context.Users.ToListAsync();
                if (result == null)
                    throw new NotFoundException();

                var users = mapper.Map<List<GetAllAvailableCustomerUsersDto>>(result);
                var userList = new List<GetAllAvailableCustomerUsersDto>();
                foreach (var user in users)
                {
                    if (await context.Customers.AnyAsync(o => o.UserId == user.UserId))
                        continue;

                    var userInRole = await context.UserInRoles.SingleOrDefaultAsync(o => o.UserId == user.UserId);
                    if (userInRole == null)
                        continue;

                    var role = await context.Roles.FindAsync(userInRole.RoleId);
                    if (role == null)
                        continue;

                    user.RoleName = role.RoleName;

                    if (user.RoleName.ToLower() == "customer")
                        userList.Add(user);
                }

                return userList.OrderByDescending(o => o.UserId).ToList();
            }
        }
    }
}
