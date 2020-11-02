using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateBookingHistoryMapper : Profile
    {
        public UpdateBookingHistoryMapper()
        {
            CreateMap<BookingHistory, UpdateBookingHistoryDto>();
            CreateMap<UpdateBookingHistoryDto, BookingHistory>();
        }
    }
}
