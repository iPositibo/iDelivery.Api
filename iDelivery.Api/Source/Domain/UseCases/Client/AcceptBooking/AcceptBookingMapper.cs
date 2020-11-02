using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class AcceptBookingMapper : Profile
    {
        public AcceptBookingMapper()
        {
            CreateMap<Booking, AcceptBookingDto>();
            CreateMap<AcceptBookingDto, AcceptBookingDto>();
        }
    }
}
