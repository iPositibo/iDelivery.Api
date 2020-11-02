using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteAppRatingCommand : IRequest
    {
        public int AppRatingId { get; set; }

        public DeleteAppRatingCommand(int appRatingId) => this.AppRatingId = appRatingId;

        private class RequestHandler : IRequestHandler<DeleteAppRatingCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteAppRatingCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.AppRatings.FindAsync(request);
                if (entity == null)
                    throw new NotFoundException();

                context.Remove(entity);
                context.SaveChanges();

                return Unit.Value;
            }
        }
    }
}
