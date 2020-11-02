using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using iDelivery.Api.Source.Infrastructure.Helpers;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateUserCommand : IRequest<CreateUserDto>
    {
        public CreateUserDto Dto { get; }

        public CreateUserCommand(CreateUserDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateUserCommand, CreateUserDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                // validation
                if (string.IsNullOrWhiteSpace(request.Dto.Password))
                    throw new PasswordIsRequiredException();

                if (context.Users.Where(o => o.Username == request.Dto.Username).Any())
                    throw new UniqueConsraintException();

                byte[] passwordHash, passwordSalt;
                AuthHelper.CreatePasswordHash(request.Dto.Password, out passwordHash, out passwordSalt);

                var user = new User
                {
                    Username = request.Dto.Username,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                context.Add(user);
                await context.SaveChangesAsync();

                var userRole = new UserInRole
                {
                    UserId = user.UserId,
                    RoleId = request.Dto.RoleId
                };

                context.UserInRoles.Add(userRole);
                await context.SaveChangesAsync();

                var result = mapper.Map<CreateUserDto>(user);
                return result;
            }
        }
    }
}
