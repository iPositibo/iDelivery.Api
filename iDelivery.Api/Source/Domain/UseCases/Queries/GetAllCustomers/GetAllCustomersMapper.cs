using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries.GetAllCustomers
{
    public class GetAllCustomersMapper : Profile
    {
        public GetAllCustomersMapper()
        {
            CreateMap<Customer, GetAllCustomersDto>();
            CreateMap<GetAllCustomersDto, Customer>();
        }
    }
}
