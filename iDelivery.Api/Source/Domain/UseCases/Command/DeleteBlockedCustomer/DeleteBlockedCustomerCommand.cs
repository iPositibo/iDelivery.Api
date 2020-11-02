using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteBlockedCustomerCommand : IRequest
    {
        public int CustomerId { get; set; }

        public DeleteBlockedCustomerCommand(int customerId) => this.CustomerId = customerId;

        private class RequestHandler : IRequestHandler<DeleteBlockedCustomerCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteBlockedCustomerCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.BlockedCustomers.FindAsync(request.CustomerId);
                if (entity == null)
                {
                    throw new NotFoundException();
                }

                context.Remove(entity);
                await context.SaveChangesAsync();

                // set the customer status to active again.
                var customer = await context.Customers.FindAsync(request.CustomerId);
                if (customer != null)
                {
                    var customerStatus = await context.CustomerStatus.SingleOrDefaultAsync(o => o.Status.ToLower() == "active");
                    if (customerStatus != null)
                        customer.CustomerStatusId = customerStatus.CustomerStatusId;

                    context.Customers.Update(customer);
                    await context.SaveChangesAsync();
                }

                return Unit.Value;
            }
        }
    }
}
