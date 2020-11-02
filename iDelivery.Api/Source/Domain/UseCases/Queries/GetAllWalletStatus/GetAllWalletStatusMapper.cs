using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllWalletStatusMapper : Profile
    {
        public GetAllWalletStatusMapper()
        {
            CreateMap<WalletStatus, GetAllWalletStatusDto>();
            CreateMap<GetAllWalletStatusDto, WalletStatus>();
        }
    }
}
