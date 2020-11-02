using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllCustomerStatusMapper : Profile
    {
        public GetAllCustomerStatusMapper()
        {
            CreateMap<CustomerStatus, GetAllCustomerStatusDto>();
            CreateMap<GetAllCustomerStatusDto, CustomerStatus>();
        }
    }
}
