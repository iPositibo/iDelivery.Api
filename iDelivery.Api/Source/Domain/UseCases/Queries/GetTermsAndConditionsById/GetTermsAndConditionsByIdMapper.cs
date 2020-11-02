using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetTermsAndConditionsByIdMapper : Profile
    {
        public GetTermsAndConditionsByIdMapper()
        {
            CreateMap<TermsAndCondition, GetTermsAndConditionsByIdDto>();
            CreateMap<GetTermsAndConditionsByIdDto, TermsAndCondition>();
        }
    }
}
