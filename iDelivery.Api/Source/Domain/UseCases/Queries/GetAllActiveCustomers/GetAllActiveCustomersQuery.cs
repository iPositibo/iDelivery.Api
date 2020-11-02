using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllActiveCustomersQuery : IRequest<List<GetAllActiveCustomersDto>>
    {
        private class GetAllNotBlockedCustomersQueryHandler : IRequestHandler<GetAllActiveCustomersQuery, List<GetAllActiveCustomersDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllNotBlockedCustomersQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllActiveCustomersDto>> Handle(GetAllActiveCustomersQuery request, CancellationToken cancellationToken)
            {
                // get all active customers.
                var status = await context.CustomerStatus.SingleOrDefaultAsync(o => o.Status.ToLower() == "active");
                if (status == null)
                    throw new NotFoundException();

                var result = await context.Customers.Where(o => o.CustomerStatusId == status.CustomerStatusId).ToListAsync();
                if (result == null)
                    throw new ActiveCustomerNotFoundException();

                var customers = mapper.Map<List<GetAllActiveCustomersDto>>(result);
                if (customers != null)
                {
                    foreach (var customer in customers)
                    {
                        customer.CustomerName = $"{ customer.LastName }, { customer.FirstName }";

                        if (customer.RatingId > 0)
                        {
                            var customerRating = await context.CustomerRatings.FindAsync(customer.RatingId);
                            if (customerRating != null)
                                customer.Rating = customerRating.Rating.ToString();
                        }

                        if (customer.CustomerStatusId > 0)
                        {
                            var customerStatus = await context.CustomerStatus.FindAsync(customer.CustomerStatusId);
                            if (customerStatus != null)
                                customerStatus.Status = customerStatus.Status;
                        }
                    }
                }

                return customers;
            }
        }
    }
}
