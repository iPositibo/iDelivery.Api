using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateVehicleDetailCommand : IRequest<CreateVehicleDetailDto>
    {
        public CreateVehicleDetailDto Dto { get; }

        public CreateVehicleDetailCommand(CreateVehicleDetailDto dto) => this.Dto = dto;

        private class RequestHandler : IRequestHandler<CreateVehicleDetailCommand, CreateVehicleDetailDto>
        {
            private DataContext context;
            private IMapper mapper;

            public RequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CreateVehicleDetailDto> Handle(CreateVehicleDetailCommand request, CancellationToken cancellationToken)
            {
                var entity = mapper.Map<VehicleDetail>(request.Dto);

                if(await context.VehicleDetails.AnyAsync(o => o.RiderId == entity.RiderId ))
                    throw new RiderVehicleAlreadyRegisteredException();

                context.VehicleDetails.Add(entity);
                await context.SaveChangesAsync();

                var result = mapper.Map<CreateVehicleDetailDto>(entity);
                return result;
            }
        }
    }
}
