using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllExternalAccountsMapper : Profile
    {
        public GetAllExternalAccountsMapper()
        {
            CreateMap<ExternalAccount, GetAllExternalAccountsDto>();
            CreateMap<GetAllExternalAccountsDto, ExternalAccount>();
        }
    }
}
