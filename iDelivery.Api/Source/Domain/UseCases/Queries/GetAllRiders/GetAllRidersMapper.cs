using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllRidersMapper : Profile
    {
        public GetAllRidersMapper()
        {
            CreateMap<Rider, GetAllRidersDto>();
            CreateMap<GetAllRidersDto, Rider>();
        }
    }
}
