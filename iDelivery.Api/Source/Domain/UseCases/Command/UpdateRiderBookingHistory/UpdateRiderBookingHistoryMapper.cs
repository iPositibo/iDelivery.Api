using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateRiderBookingHistoryMapper : Profile
    {
        public UpdateRiderBookingHistoryMapper()
        {
            CreateMap<RiderBookingHistory, UpdateRiderBookingHistoryDto>();
            CreateMap<UpdateRiderBookingHistoryDto, RiderBookingHistory>();
        }
    }
}
