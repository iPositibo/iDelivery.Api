using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteCustomerRatingCommand : IRequest
    {
        public int CustomerRatingId { get; set; }

        public DeleteCustomerRatingCommand(int customerRatingId) => this.CustomerRatingId = customerRatingId;

        private class RequestHandler : IRequestHandler<DeleteCustomerRatingCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteCustomerRatingCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.CustomerRatings.FindAsync(request.CustomerRatingId);
                if (entity == null)
                    throw new NotFoundException();

                context.Remove(entity);
                context.SaveChanges();

                return Unit.Value;
            }
        }
    }
}
