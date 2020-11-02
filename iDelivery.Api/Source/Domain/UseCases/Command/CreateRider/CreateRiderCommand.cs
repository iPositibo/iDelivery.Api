using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateRiderCommand : IRequest<CreateRiderDto>
    {
        public CreateRiderDto Dto { get; }

        public CreateRiderCommand(CreateRiderDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateRiderCommand, CreateRiderDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateRiderDto> Handle(CreateRiderCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<Rider>(request.Dto);
                context.Riders.Add(entity);
                await context.SaveChangesAsync();

                var result = mapper.Map<CreateRiderDto>(entity);
                return result;
            }
        }
    }
}
