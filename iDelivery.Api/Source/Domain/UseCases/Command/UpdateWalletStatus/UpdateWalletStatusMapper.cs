using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateWalletStatusMapper : Profile
    {
        public UpdateWalletStatusMapper()
        {
            CreateMap<WalletStatus, UpdateWalletStatusDto>();
            CreateMap<UpdateWalletStatusDto, WalletStatus>();
        }
    }
}
