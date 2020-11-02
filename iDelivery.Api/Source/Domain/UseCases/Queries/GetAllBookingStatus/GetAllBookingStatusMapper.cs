using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllBookingStatusMapper : Profile
    {
        public GetAllBookingStatusMapper()
        {
            CreateMap<BookingStatus, GetAllBookingStatusDto>();
            CreateMap<GetAllBookingStatusDto, BookingStatus>();
        }
    }
}
