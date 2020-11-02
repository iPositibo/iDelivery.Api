using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using iDelivery.Api.Source.Infrastructure.Helpers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client.Authentication
{
    public class ChangePasswordCommand : IRequest<ChangePasswordDto>
    {
        public int UserId { get; set; }
        public string Password { get; set; }

        public ChangePasswordCommand(int userId, string password)
        {
            this.UserId = userId;
            this.Password = password;
        }

        public class RequestHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<ChangePasswordDto> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
            {
                var user = await context.Users.FindAsync(request.UserId);
                if (user == null)
                    throw new NotFoundException();

                byte[] passwordHash, passwordSalt;
                AuthHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                context.Users.Update(user);
                context.SaveChanges();

                return mapper.Map<ChangePasswordDto>(user);
            }
        }
    }
}
