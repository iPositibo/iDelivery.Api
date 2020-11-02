using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateCustomerStatusCommand : IRequest<CreateCustomerStatusDto>
    {
        public CreateCustomerStatusDto Dto { get; }

        public CreateCustomerStatusCommand(CreateCustomerStatusDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateCustomerStatusCommand, CreateCustomerStatusDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateCustomerStatusDto> Handle(CreateCustomerStatusCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<CustomerStatus>(request.Dto);

                context.CustomerStatus.Add(entity);
                await context.SaveChangesAsync();

                var result = mapper.Map<CreateCustomerStatusDto>(entity);
                return result;
            }
        }
    }
}
