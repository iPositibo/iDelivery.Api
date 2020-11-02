using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetBookingHistoryByIdMapper : Profile
    {
        public GetBookingHistoryByIdMapper()
        {
            CreateMap<BookingHistory, GetBookingByIdDto>();
            CreateMap<GetBookingByIdDto, BookingHistory>();
        }
    }
}
