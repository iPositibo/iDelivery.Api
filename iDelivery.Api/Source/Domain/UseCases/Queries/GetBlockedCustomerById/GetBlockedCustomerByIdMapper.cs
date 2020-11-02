using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetBlockedCustomerByIdMapper : Profile
    {
        public GetBlockedCustomerByIdMapper()
        {
            CreateMap<BlockedCustomer, GetBlockedCustomerByIdDto>();
            CreateMap<GetBlockedCustomerByIdDto, BlockedCustomer>();
        }
    }
}
