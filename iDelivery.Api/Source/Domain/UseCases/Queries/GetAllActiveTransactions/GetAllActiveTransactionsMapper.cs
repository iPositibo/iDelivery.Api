using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllActiveTransactionsMapper : Profile
    {
        public GetAllActiveTransactionsMapper()
        {
            CreateMap<Booking, GetAllActiveTransactionsDto>();
            CreateMap<GetAllActiveTransactionsDto, Booking>();
        }
    }
}
