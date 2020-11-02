using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllBookingsMapper : Profile
    {
        public GetAllBookingsMapper()
        {
            CreateMap<Booking, GetAllBookingsDto>();
            CreateMap<GetAllBookingsDto, Booking>();
        }
    }
}
