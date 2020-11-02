using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetBlockedCustomerByIdQuery : IRequest<GetBlockedCustomerByIdDto>
    {
        public int Id { get; }

        public GetBlockedCustomerByIdQuery(int id) => this.Id = id;

        private class GetBlockedCustomerByIdQueryHandler : IRequestHandler<GetBlockedCustomerByIdQuery, GetBlockedCustomerByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetBlockedCustomerByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetBlockedCustomerByIdDto> Handle(GetBlockedCustomerByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await context.BlockedCustomers.FindAsync(request.Id);
                var blockedCustomer = mapper.Map<GetBlockedCustomerByIdDto>(result);

                var customer = await context.Customers.FindAsync(blockedCustomer.CustomerId);
                if (customer != null)
                    blockedCustomer.CustomerName = $"{ customer.LastName }, {customer.FirstName}";

                blockedCustomer.DateBlockedFormatted = blockedCustomer.DateBlocked.ToString("MM/dd/yyyy");

                return blockedCustomer;
            }
        }
    }
}
