using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class DeleteVehicleDetailCommand : IRequest
    {
        public int VehicleDetailId { get; set; }

        public DeleteVehicleDetailCommand(int vehicleDetailId) => this.VehicleDetailId = vehicleDetailId;

        private class RequestHandler : IRequestHandler<DeleteVehicleDetailCommand>
        {
            private DataContext context;

            public RequestHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteVehicleDetailCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.VehicleDetails.FindAsync(request.VehicleDetailId);
                if (entity == null)
                    throw new NotFoundException();

                context.Remove(entity);
                context.SaveChanges();

                return Unit.Value;
            }
        }
    }
}
