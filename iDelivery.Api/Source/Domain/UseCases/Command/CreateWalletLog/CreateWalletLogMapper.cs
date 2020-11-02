using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateWalletLogMapper : Profile
    {
        public CreateWalletLogMapper()
        {
            CreateMap<WalletLog, CreateWalletLogDto>();
            CreateMap<CreateWalletLogDto, WalletLog>();
        }
    }
}
