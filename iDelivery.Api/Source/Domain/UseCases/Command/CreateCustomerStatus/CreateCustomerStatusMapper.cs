using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateCustomerStatusMapper : Profile
    {
        public CreateCustomerStatusMapper()
        {
            CreateMap<CustomerStatus, CreateCustomerStatusDto>();
            CreateMap<CreateCustomerStatusDto, CustomerStatus>();
        }
    }
}
