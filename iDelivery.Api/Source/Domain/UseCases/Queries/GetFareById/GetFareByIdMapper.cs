using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetFareByIdMapper : Profile
    {
        public GetFareByIdMapper()
        {
            CreateMap<Fare, GetFareByIdDto>();
            CreateMap<GetFareByIdDto, Fare>();
        }
    }
}
