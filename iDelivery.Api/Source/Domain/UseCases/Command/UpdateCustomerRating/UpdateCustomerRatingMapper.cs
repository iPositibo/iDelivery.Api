using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateCustomerRatingMapper : Profile
    {
        public UpdateCustomerRatingMapper()
        {
            CreateMap<CustomerRating, UpdateCustomerRatingDto>();
            CreateMap<UpdateCustomerRatingDto, CustomerRating>();
        }
    }
}
