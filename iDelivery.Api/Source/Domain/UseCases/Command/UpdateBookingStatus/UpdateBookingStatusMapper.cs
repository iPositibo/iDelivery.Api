using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateBookingStatusMapper : Profile
    {
        public UpdateBookingStatusMapper()
        {
            CreateMap<BookingStatus, UpdateBookingStatusDto>();
            CreateMap<UpdateBookingStatusDto, BookingStatus>();
        }
    }
}
