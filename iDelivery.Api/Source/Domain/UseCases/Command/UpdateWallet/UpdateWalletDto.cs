using System;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateWalletDto
    {
        public int RiderId { get; set; }
        public string RiderName { get; set; }
        public decimal CurrentPoints { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal PointsLoaded { get; set; }
        public decimal? NegativeBalance { get; set; }
        public int WalletStatusId { get; set; }
    }
}
