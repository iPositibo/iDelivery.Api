using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllWalletLogsMapper : Profile
    {
        public GetAllWalletLogsMapper()
        {
            CreateMap<WalletLog, GetAllWalletLogsDto>();
            CreateMap<GetAllWalletLogsDto, WalletLog>();
        }
    }
}
