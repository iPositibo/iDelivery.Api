using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateAppRatingMapper : Profile
    {
        public CreateAppRatingMapper()
        {
            CreateMap<AppRating, CreateAppRatingDto>();
            CreateMap<CreateAppRatingDto, AppRating>();
        }
    }
}
