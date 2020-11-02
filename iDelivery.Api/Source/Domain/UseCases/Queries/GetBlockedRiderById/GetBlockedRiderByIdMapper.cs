using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetBlockedRiderByIdMapper : Profile
    {
        public GetBlockedRiderByIdMapper()
        {
            CreateMap<BlockedRider, GetBlockedCustomerByIdDto>();
            CreateMap<GetBlockedCustomerByIdDto, BlockedRider>();
        }
    }
}
