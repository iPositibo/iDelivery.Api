using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateVehicleDetailMapper : Profile
    {
        public CreateVehicleDetailMapper()
        {
            CreateMap<VehicleDetail, CreateVehicleDetailDto>();
            CreateMap<CreateVehicleDetailDto, VehicleDetail>();
        }
    }
}
