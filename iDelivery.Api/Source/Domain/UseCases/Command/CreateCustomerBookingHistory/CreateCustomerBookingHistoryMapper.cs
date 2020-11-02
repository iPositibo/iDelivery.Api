using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateCustomerBookingHistoryMapper : Profile
    {
        public CreateCustomerBookingHistoryMapper()
        {
            CreateMap<CustomerBookingHistory, CreateCustomerBookingHistoryDto>();
            CreateMap<CreateCustomerBookingHistoryDto, CustomerBookingHistory>();
        }
    }
}
