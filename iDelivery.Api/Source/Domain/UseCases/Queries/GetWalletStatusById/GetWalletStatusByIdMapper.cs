using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetWalletStatusByIdMapper : Profile
    {
        public GetWalletStatusByIdMapper()
        {
            CreateMap<WalletStatus, GetAllWalletStatusDto>();
            CreateMap<GetAllWalletStatusDto, WalletStatus>();
        }
    }
}
