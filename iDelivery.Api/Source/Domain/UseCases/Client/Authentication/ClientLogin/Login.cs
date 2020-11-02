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
    public class Login : IRequest<LoginResultDto>
    {
        public LoginDto Dto { get; set; }

        public Login(LoginDto dto) => this.Dto = dto;

        public class RequestHandler : IRequestHandler<Login, LoginResultDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<LoginResultDto> Handle(Login request, CancellationToken cancellationToken)
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
                if(result.Roles.Any(o => o.RoleName.ToLower() == "customer"))
                {
                    var customer = await context.Customers.SingleOrDefaultAsync(o => o.UserId == user.UserId);
                    if (customer != null)
                    {
                        result.LastName = customer.LastName;
                        result.FirstName = customer.FirstName;
                        result.Email = customer.Email;
                        result.ContactNumber = customer.ContactNumber;
                        result.Address = customer.Address;
                        result.IsActiveEmailReceipt = customer.ActivateEmailReceipts;
                        result.PhotoUrl = customer.PhotoUrl;
                        result.IsVerified = customer.IsVerified.GetValueOrDefault();
                    }
                }
                else if(result.Roles.Any(o => o.RoleName.ToLower() == "rider"))
                {
                    var rider = await context.Riders.SingleOrDefaultAsync(o => o.UserId == user.UserId);
                    if (rider != null)
                    {
                        result.LastName = rider.LastName;
                        result.FirstName = rider.FirstName;
                        result.Email = rider.Email;
                        result.Address = rider.Address;
                        result.ContactNumber = rider.ContactNumber;
                        result.PhotoUrl = rider.PhotoUrl;

                        var vehicleDetail = await context.VehicleDetails.SingleOrDefaultAsync(o => o.RiderId == rider.UserId);
                        result.VehicleDetail = mapper.Map<VehicleDetailDto>(vehicleDetail);
                    }
                }

                var entity = await context.Users.FindAsync(user.UserId);
                if (entity != null)
                {
                    if (!string.IsNullOrEmpty(result.Token))
                    {
                        entity.Token = result.Token;
                        context.Users.Update(entity);
                        await context.SaveChangesAsync();
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
