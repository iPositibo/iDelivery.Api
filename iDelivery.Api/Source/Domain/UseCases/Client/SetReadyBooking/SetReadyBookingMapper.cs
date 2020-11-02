using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class SetReadyBookingMapper : Profile
    {
        public SetReadyBookingMapper()
        {
            CreateMap<Booking, SetReadyBookingDto>();
            CreateMap<SetReadyBookingDto, Booking>();
        }
    }
}
