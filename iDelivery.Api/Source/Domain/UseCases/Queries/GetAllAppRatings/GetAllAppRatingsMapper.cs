using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllAppRatingsMapper : Profile
    {
        public GetAllAppRatingsMapper()
        {
            CreateMap<AppRating, GetAllAppRatingsDto>();
            CreateMap<GetAllAppRatingsDto, AppRating>();
        }
    }
}
