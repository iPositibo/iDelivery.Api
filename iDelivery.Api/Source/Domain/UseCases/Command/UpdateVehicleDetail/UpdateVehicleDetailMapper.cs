using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateVehicleDetailMapper : Profile
    {
        public UpdateVehicleDetailMapper()
        {
            CreateMap<VehicleDetail, UpdateVehicleDetailDto>();
            CreateMap<UpdateVehicleDetailDto, VehicleDetail>();
        }
    }
}
