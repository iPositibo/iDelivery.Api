using System;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetWalletByIdDto
    {
        public int WalletId { get; set; }
        public int RiderId { get; set; }
        public string RiderName { get; set; }
        public decimal CurrentPoints { get; set; }
        public int WalletStatusId { get; set; }
        public string Status { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string DateUpdatedFormatted { get; set; }
        public DateTime DateCreated { get; set; }
        public string DateCreatedFormatted { get; set; }
        public decimal PointsLoaded { get; set; }
        public decimal? NegativeBalance { get; set; }
    }
}
