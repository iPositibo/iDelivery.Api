using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateCustomerStatusMapper : Profile
    {
        public UpdateCustomerStatusMapper()
        {
            CreateMap<CustomerStatus, UpdateCustomerStatusDto>();
            CreateMap<UpdateCustomerStatusDto, CustomerStatus>();
        }
    }
}
