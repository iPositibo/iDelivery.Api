using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllowedLocationByIdMapper : Profile
    {
        public GetAllowedLocationByIdMapper()
        {
            CreateMap<AllowedLocation, GetAllowedLocationByIdDto>();
            CreateMap<GetAllowedLocationByIdDto, AllowedLocation>();
        }
    }
}
