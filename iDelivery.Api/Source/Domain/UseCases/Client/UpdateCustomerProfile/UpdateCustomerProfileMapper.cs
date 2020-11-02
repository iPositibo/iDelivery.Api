using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class UpdateCustomerProfileMapper : Profile
    {
        public UpdateCustomerProfileMapper()
        {
            CreateMap<Customer, UpdateCustomerProfileDto>();
            CreateMap<UpdateCustomerProfileDto, Customer>();
        }
    }
}
