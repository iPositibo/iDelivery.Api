using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class SetCustomerLocationMapper : Profile
    {
        public SetCustomerLocationMapper()
        {
            CreateMap<Customer, SetCustomerLocationDto>();
            CreateMap<SetCustomerLocationDto, Customer>();
        }
    }
}
