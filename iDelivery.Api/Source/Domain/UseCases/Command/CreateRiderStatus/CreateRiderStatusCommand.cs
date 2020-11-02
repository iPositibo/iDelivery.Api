using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateRiderStatusCommand : IRequest<CreateRiderStatusDto>
    {
        public CreateRiderStatusDto Dto { get; }

        public CreateRiderStatusCommand(CreateRiderStatusDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateRiderStatusCommand, CreateRiderStatusDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateRiderStatusDto> Handle(CreateRiderStatusCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<RiderStatus>(request.Dto);

                context.RiderStatus.Add(entity);
                await context.SaveChangesAsync();

                var result = mapper.Map<CreateRiderStatusDto>(entity);
                return result;
            }
        }
    }
}
