using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class PickupBookingMapper : Profile
    {
        public PickupBookingMapper()
        {
            CreateMap<Booking, PickupBookingDto>();
            CreateMap<PickupBookingDto, Booking>();
        }
    }
}
