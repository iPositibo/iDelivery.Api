using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.BusinessRules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllVehicleDetailsQuery : IRequest<List<GetAllVehicleDetailsDto>>
    {
        private class GetAllVehicleDetailsQueryHandler : IRequestHandler<GetAllVehicleDetailsQuery, List<GetAllVehicleDetailsDto>>
        {
            private DataContext context;
            private readonly IMapper mapper;

            public GetAllVehicleDetailsQueryHandler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<GetAllVehicleDetailsDto>> Handle(GetAllVehicleDetailsQuery request, CancellationToken cancellationToken)
            {
                var result = await context.VehicleDetails.ToListAsync();
                if (result == null)
                    throw new NotFoundException();

                var vehicleDetails = mapper.Map<List<GetAllVehicleDetailsDto>>(result);
                foreach(var vehicleDetail in vehicleDetails)
                {
                    var rider = await context.Riders.FindAsync(vehicleDetail.RiderId);
                    if (rider != null)
                        vehicleDetail.RiderName = $"{ rider.LastName }, {rider.FirstName}";
                }

                return vehicleDetails;
            }
        }
    }
}
