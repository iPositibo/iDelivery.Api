using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteCustomerStatusCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteCustomerStatusCommand(int id) => this.Id = id;

        private class RequestHandler : IRequestHandler<DeleteCustomerStatusCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteCustomerStatusCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.CustomerStatus.FindAsync(request.Id);
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
