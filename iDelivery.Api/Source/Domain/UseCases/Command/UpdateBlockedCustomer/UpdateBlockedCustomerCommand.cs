using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateBlockedCustomerCommand : IRequest
    {
        public int CustomerId { get; set; }
        public UpdateBlockedCustomerDto Dto { get; }

        public UpdateBlockedCustomerCommand(int customerId, UpdateBlockedCustomerDto dto)
        {
            this.CustomerId = customerId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateBlockedCustomerCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateBlockedCustomerCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<BlockedCustomer>(request.Dto);

                var blockedCustomer = await context.BlockedCustomers.FindAsync(request.CustomerId);
                if (blockedCustomer == null)
                    throw new NotFoundException();

                // update blocked customer properties
                blockedCustomer.CustomerId = entity.CustomerId;
                blockedCustomer.DateBlocked = DateTime.UtcNow;
                blockedCustomer.Reason = entity.Reason;

                context.Update(blockedCustomer);
                context.SaveChanges();

                // update customer
                var customer = await context.Customers.FindAsync(entity.CustomerId);
                if (customer != null)
                {
                    var status = await context.CustomerStatus.SingleOrDefaultAsync(o => o.Status == "Blocked");
                    if (status != null)
                        customer.CustomerStatusId = status.CustomerStatusId;
                }

                context.Update(customer);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
