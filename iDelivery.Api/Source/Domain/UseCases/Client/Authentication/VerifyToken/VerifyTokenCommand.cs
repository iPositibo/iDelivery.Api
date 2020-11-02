using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client.Authentication
{
    public class VerifyTokenCommand : IRequest<bool>
    {
        public string Token { get; set; }

        public VerifyTokenCommand(string token) => this.Token = token;

        public class RequestHandler : IRequestHandler<VerifyTokenCommand, bool>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<bool> Handle(VerifyTokenCommand request, CancellationToken cancellationToken)
            {
                if (await context.Users.AnyAsync(o => o.Token == request.Token))
                    return true;

                return false;
            }
        }
    }
}
