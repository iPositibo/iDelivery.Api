using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateWalletMapper : Profile
    {
        public CreateWalletMapper()
        {
            CreateMap<Wallet, CreateWalletDto>();
            CreateMap<CreateWalletDto, Wallet>();
        }
    }
}
