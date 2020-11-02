using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class SetRiderLocationMapper : Profile
    {
        public SetRiderLocationMapper()
        {
            CreateMap<Rider, SetRiderLocationDto>();
            CreateMap<SetRiderLocationDto, Rider>();
        }
    }
}
