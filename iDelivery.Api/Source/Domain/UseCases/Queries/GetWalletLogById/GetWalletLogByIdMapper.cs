using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetWalletLogByIdMapper : Profile
    {
        public GetWalletLogByIdMapper()
        {
            CreateMap<WalletLog, GetWalletByIdDto>();
            CreateMap<GetWalletByIdDto, WalletLog>();
        }
    }
}
