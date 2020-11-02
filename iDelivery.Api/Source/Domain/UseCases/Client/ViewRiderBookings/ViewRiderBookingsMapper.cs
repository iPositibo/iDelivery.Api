using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ViewRiderBookingsMapper : Profile
    {
        public ViewRiderBookingsMapper()
        {
            CreateMap<Booking, ViewRiderBookingsDto>();
            CreateMap<ViewRiderBookingsDto, Booking>();
        }
    }
}
