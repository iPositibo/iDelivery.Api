using AutoMapper;
using iDelivery.Api.Entities;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllWalletsMapper : Profile
    {
        public GetAllWalletsMapper()
        {
            CreateMap<Wallet, GetAllWalletsDto>();
            CreateMap<GetAllWalletsDto, Wallet>();
        }
    }
}
