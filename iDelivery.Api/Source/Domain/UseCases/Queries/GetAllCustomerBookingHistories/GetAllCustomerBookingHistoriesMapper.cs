using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllCustomerBookingHistoriesMapper : Profile
    {
        public GetAllCustomerBookingHistoriesMapper()
        {
            CreateMap<CustomerBookingHistory, GetAllCustomerBookingHistoriesDto>();
            CreateMap<GetAllCustomerBookingHistoriesDto, CustomerBookingHistory>();
        }
    }
}
