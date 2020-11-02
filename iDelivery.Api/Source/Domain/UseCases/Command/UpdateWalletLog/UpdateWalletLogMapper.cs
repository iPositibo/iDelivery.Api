using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateWalletLogMapper : Profile
    {
        public UpdateWalletLogMapper()
        {
            CreateMap<WalletLog, UpdateWalletLogDto>();
            CreateMap<UpdateWalletLogDto, WalletLog>();
        }
    }
}
