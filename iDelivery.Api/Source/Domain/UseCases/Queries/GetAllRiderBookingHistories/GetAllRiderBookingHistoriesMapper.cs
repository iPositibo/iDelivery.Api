using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllRiderBookingHistoriesMapper : Profile
    {
        public GetAllRiderBookingHistoriesMapper()
        {
            CreateMap<RiderBookingHistory, GetAllRiderBookingHistoriesDto>();
            CreateMap<GetAllRiderBookingHistoriesDto, RiderBookingHistory>();
        }
    }
}
