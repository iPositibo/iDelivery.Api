using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateAllowedLocationMapper : Profile
    {
        public CreateAllowedLocationMapper()
        {
            CreateMap<AllowedLocation, CreateAllowedLocationDto>();
            CreateMap<CreateAllowedLocationDto, AllowedLocation>();
        }
    }
}
