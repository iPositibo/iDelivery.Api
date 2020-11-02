using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteCustomerBookingHistoryCommand : IRequest
    {
        public int CustomerBookingHistoryId { get; set; }

        public DeleteCustomerBookingHistoryCommand(int customerBookingHistoryId) => this.CustomerBookingHistoryId = customerBookingHistoryId;

        private class RequestHandler : IRequestHandler<DeleteCustomerBookingHistoryCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteCustomerBookingHistoryCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.CustomerBookingHistories.FindAsync(request.CustomerBookingHistoryId);
                if (entity == null)
                {
                    throw new NotFoundException();
                }

                context.Remove(entity);
                context.SaveChanges();

                return Unit.Value;
            }
        }
    }
}
