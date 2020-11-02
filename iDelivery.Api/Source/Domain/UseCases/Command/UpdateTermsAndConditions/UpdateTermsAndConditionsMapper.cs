using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateTermsAndConditionsMapper : Profile
    {
        public UpdateTermsAndConditionsMapper()
        {
            CreateMap<TermsAndCondition, UpdateTermsAndConditionsDto>();
            CreateMap<UpdateTermsAndConditionsDto, TermsAndCondition>();
        }
    }
}
