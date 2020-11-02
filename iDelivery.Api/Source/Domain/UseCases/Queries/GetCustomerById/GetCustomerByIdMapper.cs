using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetCustomerByIdMapper :Profile
    {
        public GetCustomerByIdMapper()
        {
            CreateMap<Customer, GetCustomerByIdDto>();
            CreateMap<GetCustomerByIdDto, Customer>();
        }
    }
}
