using AutoMapper;
using iDelivery.Api.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetRiderByIdQuery : IRequest<GetRiderByIdDto>
    {
        public int Id { get; }

        public GetRiderByIdQuery(int id) => this.Id = id;

        private class GetRiderByIdQueryHandler : IRequestHandler<GetRiderByIdQuery, GetRiderByIdDto>
        {
            private DataContext context;
            private IMapper mapper;

            public GetRiderByIdQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetRiderByIdDto> Handle(GetRiderByIdQuery request, CancellationToken cancellationToken)
            { 
                var result = await context.Riders.FindAsync(request.Id);
                var rider = mapper.Map<GetRiderByIdDto>(result);

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

                return rider;
            }
        }
    }
}
