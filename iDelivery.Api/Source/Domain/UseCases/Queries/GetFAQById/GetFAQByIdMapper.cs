using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetFAQByIdMapper : Profile
    {
        public GetFAQByIdMapper()
        {
            CreateMap<Faq, GetFAQByIdDto>();
            CreateMap<GetFAQByIdDto, Faq>();
        }
    }
}
