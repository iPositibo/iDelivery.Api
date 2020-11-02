using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class RateAppMapper : Profile
    {
        public RateAppMapper()
        {
            CreateMap<AppRating, RateAppDto>();
            CreateMap<RateAppDto, AppRating>();
        }
    }
}
