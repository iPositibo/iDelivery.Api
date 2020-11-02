using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateBookingHistoryMapper : Profile
    {
        public CreateBookingHistoryMapper()
        {
            CreateMap<BookingHistory, CreateBookingHistoryDto>();
            CreateMap<CreateBookingHistoryDto, BookingHistory>();
        }
    }
}
