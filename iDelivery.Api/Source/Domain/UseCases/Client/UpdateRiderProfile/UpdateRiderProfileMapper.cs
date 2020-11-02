using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class UpdateRiderProfileMapper : Profile
    {
        public UpdateRiderProfileMapper()
        {
            CreateMap<Rider, UpdateRiderProfileDto>();
            CreateMap<UpdateRiderProfileDto, Rider>();
        }
    }
}
