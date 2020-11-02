using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using iDelivery.Api.Source.Infrastructure.Helpers;
using iDelivery.Api.Source.Infrastructure.Helpers.EmailNotification;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client.Authentication
{
    public class RegisterUser : IRequest
    {
        public RegisterUserDto Dto { get; }

        public RegisterUser(RegisterUserDto dto) => this.Dto = dto;

        public class RequestHandler : IRequestHandler<RegisterUser>
        {
            private readonly IEmailSender emailSender;
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper, IEmailSender emailSender)
            {
                this.context = context;
                this.mapper = mapper;
                this.emailSender = emailSender;
            }

            public async Task<Unit> Handle(RegisterUser request, CancellationToken cancellationToken)
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

            private async Task<bool> SendEmailVerification(string email, string verificationCode)
            {
                if (string.IsNullOrEmpty(email))
                    return false;

                var isEmailSent = false;
                var emailVerification = $"https://i-delivery.ph/emailVerification?emailcode={verificationCode}";
                var content = GetEmailContent(emailVerification);
                var emailMessage = new EmailMessage("admin@api-idelivery.com", email, content);
                try
                {
                    isEmailSent = await emailSender.SendEmailAsync(emailMessage);
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }

                return isEmailSent;
            }

            private string GenerateCode()
            {
                var alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var small_alphabets = "abcdefghijklmnopqrstuvwxyz";
                var numbers = "1234567890";

                var characters = numbers;
                characters += alphabets + small_alphabets + numbers;
                var code = string.Empty;
                for (int i = 0; i < 4; i++) // OTP code length is 4
                {
                    var character = string.Empty;
                    do
                    {
                        int index = new Random().Next(0, characters.Length);
                        character = characters.ToCharArray()[index].ToString();
                    } while (code.IndexOf(character) != -1);
                    code += character;
                }

                return code;
            }

            private async Task CreateUser(RegisterUser request, byte[] passwordHash, byte[] passwordSalt)
            {
                var user = new User
                {
                    Username = request.Dto.Username,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                context.Users.Add(user);
                await context.SaveChangesAsync();

                var isSuccess = default(bool);
                var verificationCode = GenerateCode();
                if (request.Dto.IsCustomer.GetValueOrDefault() == true)
                    isSuccess = await CreateCustomerAccount(request, user, verificationCode);

                if (isSuccess)
                    await SendEmailVerification(request.Dto.Email, verificationCode);
            }

            private async Task<bool> CreateCustomerAccount(RegisterUser request, User user, string verificationCode)
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
                    ActivateEmailReceipts = true,
                    IsVerified = false,
                    VerificationCode = verificationCode
                };

                var role = await context.Roles.SingleOrDefaultAsync(o => o.RoleName.ToLower() == "customer");
                var userRole = new UserInRole
                {
                    UserId = user.UserId,
                    RoleId = role.RoleId
                };

                // create external account data
                var externalAccount = new ExternalAccount
                {
                    AccountId = request.Dto.AccountId,
                    DateCreated = DateTime.UtcNow,
                    Type = string.Empty,
                    UserId = user.UserId
                };

                context.UserInRoles.Add(userRole);
                context.ExternalAccounts.Add(externalAccount);
                context.Customers.Add(customer);
                if (await context.SaveChangesAsync() > 0)
                    return true;

                return false;
            }

            private async Task UsernameIsAlreadyRegisteredValidation(RegisterUserDto dto)
            {
                if (await context.Users.AnyAsync(o => o.Username == dto.Username))
                {
                    throw new UsernameIsAlreadyRegisteredException();
                }
            }

            private async Task EmailIsAlreadyRegisteredValidation(RegisterUserDto dto)
            {
                if (await context.Customers.AnyAsync(o => o.Email == dto.Email))
                {
                    throw new EmailIsAlreadyRegisteredException();
                }
            }

            private void EmailAddressIsRequiredValidation(RegisterUserDto dto)
            {
                if (string.IsNullOrWhiteSpace(dto.Email))
                {
                    throw new EmailAddressIsRequiredException();
                }
            }

            private void InvalidEmailAddressFormatValidation(RegisterUserDto dto)
            {
                if (dto.Email.IsValidEmail() == false)
                    throw new InvalidEmailAddressFormatException();
            }

            private string GetEmailContent(string activationLink)
            {
                var welcomeNote = @"<html>
                                <body>
                                <p>Greetings from i-delivery Team,</p>
                                <br />
                                <p>We have received your request to reset activate your I-delivery Account. To activate simply click the activation link " + activationLink + "</p>";

                var thanksNote = @"<br />
                                   <p>Cheers,</p>
                                   <br/>
                                   i-Delivery Team";

                var footerNote = @"<br />
                                <p>---------------------------------------------------------------------------------------</p>
                                <p>This is a system generated email. Please do not reply.</p>
                                <br/>
                                <p>Connect with us! follow-i-Delivery on FACEBOOK https://www.facebook.com/iDeliveryman. If you have any questions, send an email to customersupport@i-delivery.ph or call us on 0906-062-0413(globe) or 0928-422-1861(smart)</p>
                                <br />
                                <p>Copyright C i-Delivery. All rights Reserved</p>
                                </body>
                                </html>";

                string body = string.Format("{0}{1}{2}", welcomeNote, thanksNote, footerNote);

                return body;
            }
        }
    }
}
