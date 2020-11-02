using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetCustomerStatusByIdMapper : Profile
    {
        public GetCustomerStatusByIdMapper()
        {
            CreateMap<CustomerStatus, GetCustomerStatusByIdDto>();
            CreateMap<GetCustomerStatusByIdDto, CustomerStatus>();
        }
    }
}
