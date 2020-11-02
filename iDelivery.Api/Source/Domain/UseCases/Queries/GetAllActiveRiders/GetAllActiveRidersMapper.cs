using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllActiveRidersMapper : Profile
    {
        public GetAllActiveRidersMapper()
        {
            CreateMap<Rider, GetAllActiveRidersDto>();
            CreateMap<GetAllActiveRidersDto, Rider>();
        }
    }
}
