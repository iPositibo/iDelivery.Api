using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateRiderRatingMapper : Profile
    {
        public CreateRiderRatingMapper()
        {
            CreateMap<RiderRating, CreateRiderRatingDto>();
            CreateMap<CreateRiderRatingDto, RiderRating>();
        }
    }
}
