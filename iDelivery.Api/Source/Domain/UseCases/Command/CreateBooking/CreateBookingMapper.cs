using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateBookingMapper : Profile
    {
        public CreateBookingMapper()
        {
            CreateMap<Booking, CreateBookingDto>();
            CreateMap<CreateBookingDto, Booking>();
        }
    }
}
