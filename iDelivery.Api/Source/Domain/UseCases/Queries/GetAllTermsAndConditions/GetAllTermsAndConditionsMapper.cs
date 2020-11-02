using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllTermsAndConditionsMapper : Profile
    {
        public GetAllTermsAndConditionsMapper()
        {
            CreateMap<TermsAndCondition, GetAllTermsAndConditionsDto>();
            CreateMap<GetAllTermsAndConditionsDto, TermsAndCondition>();
        }
    }
}
