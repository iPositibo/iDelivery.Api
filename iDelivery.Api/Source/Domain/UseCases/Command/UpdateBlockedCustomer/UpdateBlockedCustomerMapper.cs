using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateBlockedCustomerMapper : Profile
    {
        public UpdateBlockedCustomerMapper()
        {
            CreateMap<BlockedCustomer, UpdateBlockedCustomerDto>();
            CreateMap<UpdateBlockedCustomerDto, BlockedCustomer>();
        }
    }
}
