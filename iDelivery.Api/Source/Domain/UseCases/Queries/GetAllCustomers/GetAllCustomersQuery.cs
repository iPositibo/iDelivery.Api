using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllCustomersQuery : IRequest<List<GetAllCustomersDto>>
    {
        private class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<GetAllCustomersDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllCustomersQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllCustomersDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
            {
                var result = await context.Customers.ToListAsync();
                if (result == null)
                    throw new NotFoundException();

                var customers = mapper.Map<List<GetAllCustomersDto>>(result);
                foreach(var customer in customers)
                {
                    var customerStatus = await context.CustomerStatus.FindAsync(customer.CustomerStatusId);
                    if (customerStatus != null)
                        customer.Status = customerStatus.Status;

                    if (customer.UserId != null)
                    {
                        var user = await context.Users.FindAsync(customer.UserId);
                        if(user != null)
                            customer.Username = user.Username;
                    }
                }

                 return customers;
            }
        }
    }
}
