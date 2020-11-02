using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.UseCases.Queries;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateBookingStatusMapper : Profile
    {
        public CreateBookingStatusMapper()
        {
            CreateMap<BookingStatus, CreateBookingStatusDto>();
            CreateMap<CreateBookingStatusDto, BookingStatus>();
        }
    }
}
