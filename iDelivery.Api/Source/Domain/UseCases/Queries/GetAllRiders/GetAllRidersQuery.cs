using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllRidersQuery : IRequest<List<GetAllRidersDto>>
    {
        private class GetAllRidersQueryHandler : IRequestHandler<GetAllRidersQuery, List<GetAllRidersDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllRidersQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllRidersDto>> Handle(GetAllRidersQuery request, CancellationToken cancellationToken)
            {
                var riders = mapper.Map<List<GetAllRidersDto>>(await context.Riders.ToListAsync());
                if (riders != null)
                {
                    foreach (var rider in riders)
                    {
                        rider.RiderName = $"{ rider.LastName }, { rider.FirstName }";

                        if(rider.RatingId > 0)
                        {
                            var riderRating = await context.RiderRatings.FindAsync(rider.RatingId);
                            if (riderRating != null)
                                rider.Rating = riderRating.Rating.ToString();
                        }

                        if(rider.RiderStatusId > 0)
                        {
                            var riderStatus = await context.RiderStatus.FindAsync(rider.RiderStatusId);
                            if (riderStatus != null)
                                rider.Status = riderStatus.Status;
                        }

                        if (rider.UserId != null)
                        {
                            var user = await context.Users.FindAsync(rider.UserId);
                            rider.Username = user.Username;
                        }
                    }
                }

                return riders;
            }
        }
    }
}
