using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client.Authentication
{
    public class LoginMapper : Profile
    {
        public LoginMapper()
        {
            CreateMap<VehicleDetail, VehicleDetailDto>();
            CreateMap<VehicleDetailDto, VehicleDetail>();
        }
    }
}
