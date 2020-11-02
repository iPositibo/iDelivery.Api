using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateCustomerBookingHistoryMapper : Profile
    {
        public UpdateCustomerBookingHistoryMapper()
        {
            CreateMap<CustomerBookingHistory, UpdateCustomerBookingHistoryDto>();
            CreateMap<UpdateCustomerBookingHistoryDto, CustomerBookingHistory>();
        }
    }
}
