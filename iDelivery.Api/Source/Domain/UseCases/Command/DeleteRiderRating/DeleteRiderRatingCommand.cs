using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteRiderRatingCommand : IRequest
    {
        public int RiderRatingId { get; set; }

        public DeleteRiderRatingCommand(int riderRatingId) => this.RiderRatingId = riderRatingId;

        private class RequestHandler : IRequestHandler<DeleteRiderRatingCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteRiderRatingCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.RiderRatings.FindAsync(request.RiderRatingId);
                if (entity == null)
                    throw new NotFoundException();

                context.Remove(entity);
                context.SaveChanges();

                return Unit.Value;
            }
        }
    }
}
