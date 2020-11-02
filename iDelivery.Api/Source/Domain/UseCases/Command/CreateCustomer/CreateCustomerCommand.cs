using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateCustomerCommand : IRequest<CreateCustomerDto>
    {
        public CreateCustomerDto Dto { get; }

        public CreateCustomerCommand(CreateCustomerDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateCustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<Customer>(request.Dto);

                context.Customers.Add(entity);
                await context.SaveChangesAsync();

                var result = mapper.Map<CreateCustomerDto>(entity);
                return result;
            }
        }
    }
}
