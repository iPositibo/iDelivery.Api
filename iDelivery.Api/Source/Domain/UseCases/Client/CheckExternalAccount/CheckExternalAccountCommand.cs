using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using iDelivery.Api.Source.Domain.UseCases.Client.Authentication;
using iDelivery.Api.Source.Infrastructure.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class CheckExternalAccountCommand : IRequest<LoginResultDto>
    {
        public string AccountId { get; }

        public CheckExternalAccountCommand(string accountId) => this.AccountId = accountId;

        private class CheckExternalAccountHandler : IRequestHandler<CheckExternalAccountCommand, LoginResultDto>
        {
            private DataContext context;
            private IMapper mapper;

            public CheckExternalAccountHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<LoginResultDto> Handle(CheckExternalAccountCommand request, CancellationToken cancellationToken)
            {
                var externalAccount = await context.ExternalAccounts.SingleOrDefaultAsync(o => o.AccountId == request.AccountId);
                if (externalAccount == null)
                    throw new NotFoundException();

                var user = await context.Users.FindAsync(externalAccount.UserId);
                if (user == null)
                    throw new UserNotExistException();

                var result = await Login(user);

                return result;
            }

            private async Task<LoginResultDto> Login(User user)
            {
                // authentication is successful
                LoginResultDto result = new LoginResultDto
                {
                    Id = user.UserId,
                    Username = user.Username,
                    Token = GenerateJwtToken(user)
                };

                var roles = new List<RoleDto>();
                var userInRoles = await context.UserInRoles.Where(o => o.UserId == user.UserId).ToListAsync();
                if (userInRoles != null)
                {
                    foreach (var role in userInRoles)
                    {
                        var item = await context.Roles.FindAsync(role.RoleId);
                        if (item != null)
                        {
                            roles.Add(new RoleDto
                            {
                                RoleId = item.RoleId,
                                RoleName = item.RoleName
                            });
                        }
                    }
                }

                result.Roles = roles;
                if (result.Roles.Any(o => o.RoleName.ToLower() == "customer"))
                {
                    var customer = await context.Customers.SingleOrDefaultAsync(o => o.UserId == user.UserId);
                    if (customer != null)
                    {
                        result.LastName = customer.LastName;
                        result.FirstName = customer.FirstName;
                        result.Email = customer.Email;
                        result.ContactNumber = customer.ContactNumber;
                    }
                }

                return result;
            }

            private string GenerateJwtToken(User user)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(AuthHelper.SECRET);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.UserId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return tokenString;
            }
        }
    }
}
