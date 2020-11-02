using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllCustomerRatingsMapper : Profile
    {
        public GetAllCustomerRatingsMapper()
        {
            CreateMap<CustomerRating, GetAllCustomerRatingsDto>();
            CreateMap<GetAllCustomerRatingsDto, CustomerRating>();
        }
    }
}
