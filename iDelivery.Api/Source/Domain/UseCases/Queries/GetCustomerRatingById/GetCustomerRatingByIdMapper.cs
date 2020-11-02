using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetCustomerRatingByIdMapper : Profile
    {
        public GetCustomerRatingByIdMapper()
        {
            CreateMap<CustomerRating, GetCustomerRatingByIdDto>();
            CreateMap<GetCustomerRatingByIdDto, CustomerRating>();
        }
    }
}
