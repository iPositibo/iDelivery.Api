using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
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

namespace iDelivery.Api.Source.Domain.UseCases.Client.Authentication
{
    public class LoginAdminCommand : IRequest<LoginResultDto>
    {
        public LoginAdminDto Dto { get; set; }

        public LoginAdminCommand(LoginAdminDto dto) => this.Dto = dto;

        public class RequestHandler : IRequestHandler<LoginAdminCommand, LoginResultDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<LoginResultDto> Handle(LoginAdminCommand request, CancellationToken cancellationToken)
            {
                var user = await context.Users.SingleOrDefaultAsync(o => o.Username == request.Dto.Username);
                if (user == null)
                    throw new UsernamePasswordIncorrectException();

                // check if password is correct
                if (!AuthHelper.VerifyPasswordHash(request.Dto.Password, user.PasswordHash, user.PasswordSalt))
                    throw new UsernamePasswordIncorrectException();

                // authentication is successful
                LoginResultDto result = new LoginResultDto
                {
                    Id = user.UserId,
                    Username = user.Username,
                    Token = GenerateJwtToken(user)
                };

                var roles = new List<RoleDto>();
                var userInRoles = await context.UserInRoles.Where(o => o.UserId == user.UserId).ToListAsync();
                if (userInRoles == null)
                    throw new AccountNotAllowedException();

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

                if (roles.Any(o => o.RoleName.Contains("customer")) && roles.Any(o => o.RoleName.Contains("rider")))
                    throw new AccountNotAllowedException();

                result.Roles = roles;
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
