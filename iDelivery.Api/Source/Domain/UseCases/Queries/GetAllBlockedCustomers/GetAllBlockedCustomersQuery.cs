using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllBlockedCustomersQuery : IRequest<List<GetAllBlockedCustomersDto>>
    {
        private class GetAllBlockedCustomersQueryHandler : IRequestHandler<GetAllBlockedCustomersQuery, List<GetAllBlockedCustomersDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllBlockedCustomersQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllBlockedCustomersDto>> Handle(GetAllBlockedCustomersQuery request, CancellationToken cancellationToken)
            {
                var result = mapper.Map<List<GetAllBlockedCustomersDto>>(await context.BlockedCustomers.ToListAsync());

                foreach (var item in result)
                {
                    var customer = await context.Customers.FindAsync(item.CustomerId);
                    if (customer != null)
                        item.CustomerName = $"{ customer.LastName }, {customer.FirstName}";

                    item.DateBlockedFormatted = item.DateBlocked.ToString("MM/dd/yyyy");
                }

                return result;
            }
        }
    }
}
