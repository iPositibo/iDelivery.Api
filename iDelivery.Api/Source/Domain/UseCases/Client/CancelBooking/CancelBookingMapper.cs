using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class CancelBookingMapper : Profile
    {
        public CancelBookingMapper()
        {
            CreateMap<Booking, CancelBookingDto>();
            CreateMap<CancelBookingDto, Booking>();
        }
    }
}
