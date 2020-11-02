using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllFAQsMapper : Profile
    {
        public GetAllFAQsMapper()
        {
            CreateMap<Faq, GetAllFAQsDto>();
            CreateMap<GetAllFAQsDto, Faq>();
        }
    }
}
