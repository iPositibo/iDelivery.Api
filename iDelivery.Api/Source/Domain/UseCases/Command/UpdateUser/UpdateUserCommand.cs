﻿using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using iDelivery.Api.Source.Infrastructure.Helpers;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateUserCommand : IRequest
    {
        public int UserId { get; set; }
        public UpdateUserDto Dto { get; }

        public UpdateUserCommand(int userId, UpdateUserDto dto)
        {
            this.UserId = userId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateUserCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<User>(request.Dto);

                var user = await context.Users.FindAsync(request.UserId);
                if (user == null)
                    throw new NotFoundException();


                if (entity.Username != user.Username)
                {
                    // username has changed so check if the new username is already taken
                    if (context.Users.Any(x => x.Username == entity.Username))
                        throw new UniqueConsraintException();
                }

                // update user properties
                user.Username = entity.Username;

                // update password if it was entered
                if (!string.IsNullOrWhiteSpace(request.Dto.Password))
                {
                    byte[] passwordHash, passwordSalt;
                    AuthHelper.CreatePasswordHash(request.Dto.Password, out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                }

                context.Update(user);
                context.SaveChanges();

                // update user role
                var userRole = new UserInRole
                {
                    UserId = user.UserId,
                    RoleId = request.Dto.RoleId
                };

                context.Update(userRole);
                context.SaveChanges();

                return await System.Threading.Tasks.Task.FromResult(Unit.Value);
            }
        }
    }
}
