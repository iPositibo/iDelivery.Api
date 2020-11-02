using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetRiderBookingHistoryByIdMapper : Profile
    {
        public GetRiderBookingHistoryByIdMapper()
        {
            CreateMap<RiderBookingHistory, GetRiderBookingHistoryByIdDto>();
            CreateMap<GetRiderBookingHistoryByIdDto, RiderBookingHistory>();
        }
    }
}
