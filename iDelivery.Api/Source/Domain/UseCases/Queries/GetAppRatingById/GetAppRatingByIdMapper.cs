using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAppRatingByIdMapper : Profile
    {
        public GetAppRatingByIdMapper()
        {
            CreateMap<AppRating, GetAppRatingByIdDto>();
            CreateMap<GetAppRatingByIdDto, AppRating>();
        }
    }
}
