using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class Wallet
    {
        public int WalletId { get; set; }
        public int RiderId { get; set; }
        public decimal CurrentPoints { get; set; }
        public int WalletStatusId { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal PointsLoaded { get; set; }
        public decimal? NegativeBalance { get; set; }

        public virtual Rider Rider { get; set; }
        public virtual WalletStatus WalletStatus { get; set; }
    }
}
