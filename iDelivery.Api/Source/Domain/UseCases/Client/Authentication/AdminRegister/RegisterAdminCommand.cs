using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using iDelivery.Api.Source.Infrastructure.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client.Authentication
{
    public class RegisterAdminCommand : IRequest
    {
        public RegisterAdminDto Dto { get; }

        public RegisterAdminCommand(RegisterAdminDto dto) => this.Dto = dto;

        public class RequestHandler : IRequestHandler<RegisterAdminCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(RegisterAdminCommand request, CancellationToken cancellationToken)
            {
                EmailAddressIsRequiredValidation(request.Dto);
                InvalidEmailAddressFormatValidation(request.Dto);
                await UsernameIsAlreadyRegisteredValidation(request.Dto);
                await EmailIsAlreadyRegisteredValidation(request.Dto);

                byte[] passwordHash, passwordSalt;
                AuthHelper.CreatePasswordHash(request.Dto.Password, out passwordHash, out passwordSalt);

                await CreateUser(request, passwordHash, passwordSalt);

                return Unit.Value;
            }

            private async Task CreateUser(RegisterAdminCommand request, byte[] passwordHash, byte[] passwordSalt)
            {
                var user = new User
                {
                    Username = request.Dto.Username,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                context.Users.Add(user);
                await context.SaveChangesAsync();

                var userRole = new UserInRole
                {
                    UserId = user.UserId,
                    RoleId = request.Dto.RoleId.GetValueOrDefault()
                };

                context.UserInRoles.Add(userRole);
                await context.SaveChangesAsync();

                if (request.Dto.RoleId > 0)
                {
                    var role = await context.Roles.FindAsync(request.Dto.RoleId);
                    if (role.RoleName == "customer")
                        await CreateCustomerAccount(request, user);
                    else if (role.RoleName == "rider")
                        await CreateRiderAccount(request, user);
                }
            }

            private async Task CreateRiderAccount(RegisterAdminCommand request, User user)
            {
                var riderStatus = await context.RiderStatus.SingleOrDefaultAsync(o => o.Status.ToLower() == "active");
                if (riderStatus == null)
                    throw new RiderStatusNotFoundException();

                var rider = new Rider
                {
                    UserId = user.UserId,
                    RiderStatusId = riderStatus.RiderStatusId,
                    Email = request.Dto.Email,
                    PhotoUrl = request.Dto.PhotoUrl,
                    LastName = request.Dto.LastName,
                    FirstName = request.Dto.FirstName,
                    Address = request.Dto.Address,
                    ContactNumber = request.Dto.ContactNumber,
                };

                context.Riders.Add(rider);
                await context.SaveChangesAsync();
            }

            private async Task CreateCustomerAccount(RegisterAdminCommand request, User user)
            {
                var customerStatus = await context.CustomerStatus.SingleOrDefaultAsync(o => o.Status.ToLower() == "active");
                if (customerStatus == null)
                    throw new CustomerStatusNotFoundException();

                var customer = new Customer
                {
                    UserId = user.UserId,
                    CustomerStatusId = customerStatus.CustomerStatusId,
                    Email = request.Dto.Email,
                    PhotoUrl = request.Dto.PhotoUrl,
                    LastName = request.Dto.LastName,
                    FirstName = request.Dto.FirstName,
                    Address = request.Dto.Address,
                    ContactNumber = request.Dto.ContactNumber,
                    ActivateEmailReceipts = true
                };

                // create external account data
                var externalAccount = new ExternalAccount
                {
                    AccountId = request.Dto.AccountId,
                    DateCreated = DateTime.UtcNow,
                    Type = string.Empty,
                    UserId = user.UserId
                };

                context.ExternalAccounts.Add(externalAccount);
                context.Customers.Add(customer);
                await context.SaveChangesAsync();
            }

            private async Task UsernameIsAlreadyRegisteredValidation(RegisterAdminDto dto)
            {
                if (await context.Users.AnyAsync(o => o.Username == dto.Username))
                {
                    throw new UsernameIsAlreadyRegisteredException();
                }
            }

            private async Task EmailIsAlreadyRegisteredValidation(RegisterAdminDto dto)
            {
                if (await context.Customers.AnyAsync(o => o.Email == dto.Email))
                {
                    throw new EmailIsAlreadyRegisteredException();
                }
            }

            private void EmailAddressIsRequiredValidation(RegisterAdminDto dto)
            {
                if (string.IsNullOrWhiteSpace(dto.Email))
                {
                    throw new EmailAddressIsRequiredException();
                }
            }

            private void InvalidEmailAddressFormatValidation(RegisterAdminDto dto)
            {
                if (dto.Email.IsValidEmail() == false)
                    throw new InvalidEmailAddressFormatException();
            }
        }
    }
}
