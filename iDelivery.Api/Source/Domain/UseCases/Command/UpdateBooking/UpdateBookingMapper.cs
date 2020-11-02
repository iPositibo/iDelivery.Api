using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateBookingMapper : Profile
    {
        public UpdateBookingMapper()
        {
            CreateMap<Booking, UpdateBookingDto>();
            CreateMap<UpdateBookingDto, Booking>();
        }
    }
}
