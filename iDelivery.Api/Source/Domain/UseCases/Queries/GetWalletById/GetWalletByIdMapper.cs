using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetWalletByIdMapper : Profile
    {
        public GetWalletByIdMapper()
        {
            CreateMap<Wallet, GetWalletByIdDto>();
            CreateMap<GetWalletByIdDto, Wallet>();
        }
    }
}
