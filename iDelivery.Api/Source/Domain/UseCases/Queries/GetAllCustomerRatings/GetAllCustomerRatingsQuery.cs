using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllCustomerRatingsQuery : IRequest<List<GetAllCustomerRatingsDto>>
    {
        private class GetAllCustomerRatingsQueryHandler : IRequestHandler<GetAllCustomerRatingsQuery, List<GetAllCustomerRatingsDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllCustomerRatingsQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllCustomerRatingsDto>> Handle(GetAllCustomerRatingsQuery request, CancellationToken cancellationToken)
            {
                var ratings = mapper.Map<List<GetAllCustomerRatingsDto>>(await context.CustomerRatings.ToListAsync());
                foreach (var rating in ratings)
                {
                    var customer = await context.Customers.FindAsync(rating.CustomerId);
                    if (customer != null)
                    {
                        rating.CustomerName = $"{ customer.LastName }, { customer.FirstName }";
                        rating.DateCreatedFormatted = rating.DateCreated.ToString("MM/dd/yyyy");
                    }
                }

                return ratings;
            }
        }
    }
}
