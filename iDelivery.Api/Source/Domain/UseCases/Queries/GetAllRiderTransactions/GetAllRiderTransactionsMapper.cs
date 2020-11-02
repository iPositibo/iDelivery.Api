using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllRiderTransactionsMapper : Profile
    {
        public GetAllRiderTransactionsMapper()
        {
            CreateMap<Booking, GetAllRiderTransactionsDto>();
            CreateMap<GetAllRiderTransactionsDto, Booking>();
        }
    }
}
