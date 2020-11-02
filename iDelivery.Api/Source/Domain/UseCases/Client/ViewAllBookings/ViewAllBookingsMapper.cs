using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ViewAllBookingsMapper : Profile
    {
        public ViewAllBookingsMapper()
        {
            CreateMap<Booking, ViewAllBookingsDto>();
            CreateMap<ViewAllBookingsDto, Booking>();
        }
    }
}
