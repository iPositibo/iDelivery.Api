using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateWalletStatusMapper : Profile
    {
        public CreateWalletStatusMapper()
        {
            CreateMap<WalletStatus, CreateWalletStatusDto>();
            CreateMap<CreateWalletStatusDto, WalletStatus>();
        }
    }
}
