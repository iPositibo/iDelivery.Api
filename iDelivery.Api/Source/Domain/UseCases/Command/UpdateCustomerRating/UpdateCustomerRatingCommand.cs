using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateCustomerRatingCommand : IRequest
    {
        public int CustomerRatingId { get; set; }
        public UpdateCustomerRatingDto Dto { get; }

        public UpdateCustomerRatingCommand(int customerRatingId, UpdateCustomerRatingDto dto)
        {
            this.CustomerRatingId = customerRatingId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateCustomerRatingCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateCustomerRatingCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<CustomerRating>(request.Dto);

                var rating = await context.CustomerRatings.FindAsync(request.CustomerRatingId);
                if (rating == null)
                    throw new NotFoundException();

                // update customer rating properties
                rating.CustomerId = entity.CustomerId;
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
