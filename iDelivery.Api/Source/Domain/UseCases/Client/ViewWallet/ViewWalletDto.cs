using iDelivery.Api.Source.Domain.UseCases.Client.ViewWallet;
using iDelivery.Api.Source.Domain.UseCases.Queries;
using System;
using System.Collections.Generic;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ViewWalletDto
    {
        public int WalletId { get; set; }
        public int RiderId { get; set; }
        public string RiderName { get; set; }
        public decimal CurrentPoints { get; set; }
        public int WalletStatusId { get; set; }
        public string Status { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public string DateCreatedFormatted { get; set; }
        public decimal PointsLoaded { get; set; }
        public decimal? NegativeBalance { get; set; }
        public string TotalEarnings { get; set; }
        public decimal DeductPoints { get; set; }

        public IEnumerable<GetAllWalletLogsDto> Logs { get; set; }
        public IEnumerable<ViewRiderBookingHistoryDto> RiderBookings { get; set; }
    }
}
