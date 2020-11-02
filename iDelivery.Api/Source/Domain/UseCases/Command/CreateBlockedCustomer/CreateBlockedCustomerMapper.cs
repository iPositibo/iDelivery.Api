using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateBlockedCustomerMapper : Profile
    {
        public CreateBlockedCustomerMapper()
        {
            CreateMap<BlockedCustomer, CreateBlockedCustomerDto>();
            CreateMap<CreateBlockedCustomerDto, BlockedCustomer>();
        }
    }
}
