using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateCustomerRatingMapper : Profile
    {
        public CreateCustomerRatingMapper()
        {
            CreateMap<CustomerRating, CreateCustomerRatingDto>();
            CreateMap<CreateCustomerRatingDto, CustomerRating>();
        }
    }
}
