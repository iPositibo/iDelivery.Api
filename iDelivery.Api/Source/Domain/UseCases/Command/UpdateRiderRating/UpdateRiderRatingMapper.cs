using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateRiderRatingMapper : Profile
    {
        public UpdateRiderRatingMapper()
        {
            CreateMap<RiderRating, UpdateRiderRatingDto>();
            CreateMap<UpdateRiderRatingDto, RiderRating>();
        }
    }
}
