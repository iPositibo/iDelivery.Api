using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetExternalAccountByIdMapper : Profile
    {
        public GetExternalAccountByIdMapper()
        {
            CreateMap<ExternalAccount, GetExternalAccountByIdDto>();
            CreateMap<GetExternalAccountByIdDto, ExternalAccount>();
        }
    }
}
