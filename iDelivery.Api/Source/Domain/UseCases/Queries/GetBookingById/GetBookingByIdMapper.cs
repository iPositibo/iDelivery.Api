using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetBookingByIdMapper : Profile
    {
        public GetBookingByIdMapper()
        {
            CreateMap<Booking, GetBookingByIdDto>();
            CreateMap<GetBookingByIdDto, Booking>();
        }
    }
}
