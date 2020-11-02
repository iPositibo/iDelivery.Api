using AutoMapper;
using iDelivery.Api.Entities;
using iDelivery.Api.Source.Domain.UseCases.Client.ViewWallet;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ViewWalletMapper : Profile
    {
        public ViewWalletMapper()
        {
            CreateMap<Wallet, ViewWalletDto>();
            CreateMap<ViewWalletDto, Wallet>();

            CreateMap<ViewRiderBookingHistoryDto, RiderBookingHistory>();
            CreateMap<RiderBookingHistory, ViewRiderBookingHistoryDto>();
        }
    }
}
