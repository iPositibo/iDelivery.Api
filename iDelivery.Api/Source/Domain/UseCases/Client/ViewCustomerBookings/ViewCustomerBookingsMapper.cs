using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ViewCustomerBookingsMapper : Profile
    {
        public ViewCustomerBookingsMapper()
        {
            CreateMap<Booking, ViewCustomerBookingsDto>();
            CreateMap<ViewCustomerBookingsDto, Booking>();
        }
    }
}
