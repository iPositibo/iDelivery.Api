using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetVehicleDetailByIdDtoMapper : Profile
    {
        public GetVehicleDetailByIdDtoMapper()
        {
            CreateMap<VehicleDetail, GetVehicleDetailByIdDto>();
            CreateMap<GetVehicleDetailByIdDto, VehicleDetail>();
        }
    }
}
