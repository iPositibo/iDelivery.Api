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
    public class GetAllActiveRidersQuery : IRequest<List<GetAllActiveRidersDto>>
    {
        private class GetAllActiveRidersQueryHandler : IRequestHandler<GetAllActiveRidersQuery, List<GetAllActiveRidersDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllActiveRidersQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllActiveRidersDto>> Handle(GetAllActiveRidersQuery request, CancellationToken cancellationToken)
            {
                // get all active riders.
                var status = await context.RiderStatus.SingleOrDefaultAsync(o => o.Status.ToLower() == "active");
                if (status == null)
                    throw new NotFoundException();

                var result = await context.Riders.Where(o => o.RiderStatusId == status.RiderStatusId).ToListAsync();
                if (result == null)
                    throw new ActiveRiderNotFoundException();

                var riders = mapper.Map<List<GetAllActiveRidersDto>>(result);
                if (riders != null)
                {
                    foreach (var rider in riders)
                    {
                        rider.RiderName = $"{ rider.LastName }, { rider.FirstName }";

                        if (rider.RatingId > 0)
                        {
                            var riderRating = await context.RiderRatings.FindAsync(rider.RatingId);
                            if (riderRating != null)
                                rider.Rating = riderRating.Rating.ToString();
                        }

                        if (rider.RiderStatusId > 0)
                        {
                            var riderStatus = await context.RiderStatus.FindAsync(rider.RiderStatusId);
                            if (riderStatus != null)
                                rider.Status = riderStatus.Status;
                        }
                    }
                }

                return riders;
            }
        }
    }
}
