using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class DropOffBookingMapper : Profile
    {
        public DropOffBookingMapper()
        {
            CreateMap<Booking, DropOffBookingDto>();
            CreateMap<DropOffBookingDto, Booking>();
        }
    }
}
