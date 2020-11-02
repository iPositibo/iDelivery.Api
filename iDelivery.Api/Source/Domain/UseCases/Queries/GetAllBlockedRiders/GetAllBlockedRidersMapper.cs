using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllBlockedRidersMapper : Profile
    {
        public GetAllBlockedRidersMapper()
        {
            CreateMap<BlockedRider, GetAllBlockedRidersDto>();
            CreateMap<GetAllBlockedRidersDto, BlockedRider>();
        }
    }
}
