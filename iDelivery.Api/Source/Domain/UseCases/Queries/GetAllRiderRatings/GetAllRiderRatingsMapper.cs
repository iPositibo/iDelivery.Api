using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllRiderRatingsMapper : Profile
    {
        public GetAllRiderRatingsMapper()
        {
            CreateMap<RiderRating, GetAllRiderRatingsDto>();
            CreateMap<GetAllRiderRatingsDto, RiderRating>();
        }
    }
}
