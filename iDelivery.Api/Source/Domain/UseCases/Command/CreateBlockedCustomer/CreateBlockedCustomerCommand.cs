using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateBlockedCustomerCommand : IRequest<CreateBlockedCustomerDto>
    {
        public CreateBlockedCustomerDto Dto { get; }

        public CreateBlockedCustomerCommand(CreateBlockedCustomerDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateBlockedCustomerCommand, CreateBlockedCustomerDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateBlockedCustomerDto> Handle(CreateBlockedCustomerCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<BlockedCustomer>(request.Dto);

                // blocked customer
                var customer = await context.Customers.FindAsync(request.Dto.CustomerId);
                if (customer != null)
                {
                    var blockedCustomer = await context.BlockedCustomers.SingleOrDefaultAsync(o => o.CustomerId == request.Dto.CustomerId);
                    if (blockedCustomer != null && blockedCustomer.DateBlocked != null)
                        throw new CustomerIsAlreadyBlockedException();

                    if (blockedCustomer == null)
                    {
                        var customerStatus = await context.CustomerStatus.SingleOrDefaultAsync(o => o.Status.ToLower() == "blocked");
                        if (customerStatus != null)
                        {
                            customer.CustomerStatusId = customerStatus.CustomerStatusId;
                            context.Update(customer);
                            await context.SaveChangesAsync();

                            context.BlockedCustomers.Add(entity);
                            await context.SaveChangesAsync();
                        }
                    }
                }

                var result = mapper.Map<CreateBlockedCustomerDto>(entity);
                return result;
            }
        }
    }
}
