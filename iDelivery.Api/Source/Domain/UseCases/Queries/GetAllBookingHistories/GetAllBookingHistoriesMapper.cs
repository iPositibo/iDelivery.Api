using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllBookingHistoriesMapper : Profile
    {
        public GetAllBookingHistoriesMapper()
        {
            CreateMap<BookingHistory, GetAllBookingHistoriesDto>();
            CreateMap<GetAllBookingHistoriesDto, BookingHistory>();
        }
    }
}
