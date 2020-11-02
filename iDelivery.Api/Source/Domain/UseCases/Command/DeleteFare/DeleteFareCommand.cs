using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteFareCommand : IRequest
    {
        public int FareId { get; set; }

        public DeleteFareCommand(int fareId) => this.FareId = fareId;

        private class RequestHandler : IRequestHandler<DeleteFareCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteFareCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.Fares.FindAsync(request.FareId);
                if (entity == null)
                    throw new NotFoundException();

                if (await context.Customers.AnyAsync(o => o.FareId == request.FareId))
                    throw new FareIsCurrentlyUsedByCustomersException();

                context.Remove(entity);
                context.SaveChanges();

                return Unit.Value;
            }
        }
    }
}
