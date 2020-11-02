using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class BookDeliveryMapper : Profile
    {
        public BookDeliveryMapper()
        {
            CreateMap<Booking, BookDeliveryDto>();
            CreateMap<BookDeliveryDto, Booking>();

            CreateMap<Booking, BookDeliveryResult>();
            CreateMap<BookDeliveryResult, Booking>();
        }
    }
}
