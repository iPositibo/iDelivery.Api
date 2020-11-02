using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetCustomerBookingHistoryByIdMapper : Profile
    {
        public GetCustomerBookingHistoryByIdMapper()
        {
            CreateMap<CustomerBookingHistory, GetCustomerBookingHistoryByIdDto>();
            CreateMap<GetCustomerBookingHistoryByIdDto, CustomerBookingHistory>();
        }
    }
}
