using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateWalletMapper : Profile
    {
        public UpdateWalletMapper()
        {
            CreateMap<Wallet, UpdateWalletDto>();
            CreateMap<UpdateWalletDto, Wallet>();
        }
    }
}
