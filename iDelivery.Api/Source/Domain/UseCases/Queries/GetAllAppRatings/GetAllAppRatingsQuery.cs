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
    public class GetAllAppRatingsQuery : IRequest<List<GetAllAppRatingsDto>>
    {
        private class GetAllAppRatingsQueryHandler : IRequestHandler<GetAllAppRatingsQuery, List<GetAllAppRatingsDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllAppRatingsQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllAppRatingsDto>> Handle(GetAllAppRatingsQuery request, CancellationToken cancellationToken)
            {
                var result = await context.AppRatings.ToListAsync();
                if (result == null)
                    throw new NotFoundException();

                var ratings = mapper.Map<List<GetAllAppRatingsDto>>(result);
                foreach (var rating in ratings)
                {
                    var customer = await context.Customers.FindAsync(rating.CustomerId);
                    if (customer != null) rating.CustomerName = $"{ customer.LastName }, {customer.FirstName}";
                    rating.DateCreatedFormatted = rating.DateCreated.ToString("MM/dd/yyyy");
                }

                return ratings;
            }
        }
    }
}
