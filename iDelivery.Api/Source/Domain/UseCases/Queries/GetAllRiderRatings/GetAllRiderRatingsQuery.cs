using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllRiderRatingsQuery : IRequest<List<GetAllRiderRatingsDto>>
    {
        private class GetAllRiderRatingsQueryHandler : IRequestHandler<GetAllRiderRatingsQuery, List<GetAllRiderRatingsDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllRiderRatingsQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllRiderRatingsDto>> Handle(GetAllRiderRatingsQuery request, CancellationToken cancellationToken)
            {
                var ratings = mapper.Map<List<GetAllRiderRatingsDto>>(await context.RiderRatings.ToListAsync());
                foreach (var rating in ratings)
                {
                    var customer = await context.Riders.FindAsync(rating.RiderId);
                    if (customer != null)
                        rating.RiderName = $"{ customer.LastName }, { customer.FirstName }";

                    rating.DateCreatedFormatted = rating.DateCreated.ToString("MM/dd/yyyy");
                }

                return ratings;
            }
        }
    }
}
