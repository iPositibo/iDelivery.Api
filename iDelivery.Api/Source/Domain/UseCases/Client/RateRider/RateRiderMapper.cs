using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class RateRiderMapper : Profile
    {
        public RateRiderMapper()
        {
            CreateMap<RiderRating, RateRiderDto>();
            CreateMap<RateRiderDto, RiderRating>();
        }
    }
}
