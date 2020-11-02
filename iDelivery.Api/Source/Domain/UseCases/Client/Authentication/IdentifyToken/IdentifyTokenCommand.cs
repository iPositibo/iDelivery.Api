using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client.Authentication
{
    public class IdentifyTokenCommand : IRequest<bool>
    {
        public string Token { get; set; }

        public IdentifyTokenCommand(string token) => this.Token = token;

        public class RequestHandler : IRequestHandler<IdentifyTokenCommand, bool>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<bool> Handle(IdentifyTokenCommand request, CancellationToken cancellationToken)
            {
                return false;
            }
        }
    }
}
