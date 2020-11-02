using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class RateCustomerMapper : Profile
    {
        public RateCustomerMapper()
        {
            CreateMap<CustomerRating, RateCustomerDto>();
            CreateMap<RateCustomerDto, CustomerRating>();
        }
    }
}
