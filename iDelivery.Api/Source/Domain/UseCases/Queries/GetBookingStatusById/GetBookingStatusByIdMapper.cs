using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetBookingStatusByIdMapper : Profile
    {
        public GetBookingStatusByIdMapper()
        {
            CreateMap<BookingStatus, GetBookingStatusByIdDto>();
            CreateMap<GetBookingStatusByIdDto, BookingStatus>();
        }
    }
}
