using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ViewAllowedLocationsMapper : Profile
    {
        public ViewAllowedLocationsMapper()
        {
            CreateMap<AllowedLocation, ViewAllowedLocationsDto>();
            CreateMap<ViewAllowedLocationsDto, AllowedLocation>();
        }
    }
}
