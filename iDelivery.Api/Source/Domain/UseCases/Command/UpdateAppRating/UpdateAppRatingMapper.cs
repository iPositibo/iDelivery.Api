using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateAppRatingMapper : Profile
    {
        public UpdateAppRatingMapper()
        {
            CreateMap<AppRating, UpdateAppRatingDto>();
            CreateMap<UpdateAppRatingDto, AppRating>();
        }
    }
}
