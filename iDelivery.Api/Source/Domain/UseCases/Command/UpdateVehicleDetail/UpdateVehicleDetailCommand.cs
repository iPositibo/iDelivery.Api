using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateVehicleDetailCommand : IRequest
    {
        public int VehicleDetailId { get; set; }
        public UpdateVehicleDetailDto Dto { get; }

        public UpdateVehicleDetailCommand(int vehicleDetailId, UpdateVehicleDetailDto dto)
        {
            this.VehicleDetailId = vehicleDetailId;
            this.Dto = dto;
        }

        private class RequestHandler : IRequestHandler<UpdateVehicleDetailCommand>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateVehicleDetailCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<VehicleDetail>(request.Dto);

                var vehicleDetail = await context.VehicleDetails.FindAsync(request.VehicleDetailId);
                if (vehicleDetail == null)
                    throw new NotFoundException();

                // update vehicle details properties
                vehicleDetail.RiderId = entity.RiderId;
                vehicleDetail.Brand = entity.Brand;
                vehicleDetail.Color = entity.Color;
                vehicleDetail.Model = entity.Model;
                vehicleDetail.Orcr = entity.Orcr;
                vehicleDetail.PlateNumber = entity.PlateNumber;

                context.Update(vehicleDetail);
                context.SaveChanges();

                return await Task.FromResult(Unit.Value);
            }
        }
    }
}
