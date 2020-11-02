using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateRiderBookingHistoryMapper : Profile
    {
        public CreateRiderBookingHistoryMapper()
        {
            CreateMap<RiderBookingHistory, CreateRiderBookingHistoryDto>();
            CreateMap<CreateRiderBookingHistoryDto, RiderBookingHistory>();
        }
    }
}
