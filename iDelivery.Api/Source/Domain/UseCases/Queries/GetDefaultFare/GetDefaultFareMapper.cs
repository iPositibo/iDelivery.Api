using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetDefaultFareMapper : Profile
    {
        public GetDefaultFareMapper()
        {
            CreateMap<Fare, GetDefaultFareDto>();
            CreateMap<GetDefaultFareDto, Fare>();
        }
    }
}
