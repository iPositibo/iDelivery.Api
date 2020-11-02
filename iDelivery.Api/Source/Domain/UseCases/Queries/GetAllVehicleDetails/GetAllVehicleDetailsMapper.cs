using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllVehicleDetailsMapper : Profile
    {
        public GetAllVehicleDetailsMapper()
        {
            CreateMap<VehicleDetail, GetAllVehicleDetailsDto>();
            CreateMap<GetAllVehicleDetailsDto, VehicleDetail>();
        }
    }
}
