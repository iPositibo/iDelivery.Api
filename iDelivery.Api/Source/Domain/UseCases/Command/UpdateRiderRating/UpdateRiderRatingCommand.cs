using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateRiderRatingCommand : IRequest
    {
        public int RiderRatingId { get; set; }
        public UpdateRiderRatingDto Dto { get; }

        public UpdateRiderRatingCommand(int riderRatingId, UpdateRiderRatingDto dto)
        {
            this.RiderRatingId = riderRatingId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateRiderRatingCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateRiderRatingCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<RiderRating>(request.Dto);

                var rating = await context.RiderRatings.FindAsync(request.RiderRatingId);
                if (rating == null)
                    throw new NotFoundException();

                // update rider rating properties
                rating.RiderId = entity.RiderId;
                rating.BlockedCount = entity.BlockedCount;
                rating.ReportedCount = entity.ReportedCount;
                rating.Rating = entity.Rating;
                rating.Feedback = entity.Feedback;

                context.Update(rating);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
