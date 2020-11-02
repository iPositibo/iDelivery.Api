using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateAppRatingCommand : IRequest
    {
        public int AppRatingId { get; set; }
        public UpdateAppRatingDto Dto { get; }

        public UpdateAppRatingCommand(int appRatingId, UpdateAppRatingDto dto)
        {
            this.AppRatingId = appRatingId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateAppRatingCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateAppRatingCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<AppRating>(request.Dto);

                var appRating = await context.AppRatings.FindAsync(request.AppRatingId);
                if (appRating == null)
                    throw new NotFoundException();

                // update app rating properties
                appRating.CustomerId = entity.CustomerId;
                appRating.Rating = entity.Rating;
                appRating.Feedback = entity.Feedback;

                context.Update(appRating);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
