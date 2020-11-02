using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetRiderRatingByIdMapper : Profile
    {
        public GetRiderRatingByIdMapper()
        {
            CreateMap<RiderRating, GetRiderRatingByIdDto>();
            CreateMap<GetRiderRatingByIdDto, RiderRating>();
        }
    }
}
