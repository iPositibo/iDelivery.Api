using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllBlockedCustomersMapper : Profile
    {
        public GetAllBlockedCustomersMapper()
        {
            CreateMap<BlockedCustomer, GetAllBlockedCustomersDto>();
            CreateMap<GetAllBlockedCustomersDto, BlockedCustomer>();
        }
    }
}
