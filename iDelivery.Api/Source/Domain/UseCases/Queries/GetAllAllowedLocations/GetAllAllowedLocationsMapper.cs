using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllAllowedLocationsMapper : Profile
    {
        public GetAllAllowedLocationsMapper()
        {
            CreateMap<AllowedLocation, GetAllAllowedLocationsDto>();
            CreateMap<GetAllAllowedLocationsDto, AllowedLocation>();
        }
    }
}
