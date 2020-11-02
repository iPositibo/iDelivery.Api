using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateAllowedLocationMapper : Profile
    {
        public UpdateAllowedLocationMapper()
        {
            CreateMap<AllowedLocation, UpdateAllowedLocationDto>();
            CreateMap<UpdateAllowedLocationDto, AllowedLocation>();
        }
    }
}
