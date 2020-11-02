using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllActiveCustomersMapper : Profile
    {
        public GetAllActiveCustomersMapper()
        {
            CreateMap<Customer, GetAllActiveCustomersDto>();
            CreateMap<GetAllBlockedCustomersDto, Customer>();
        }
    }
}
