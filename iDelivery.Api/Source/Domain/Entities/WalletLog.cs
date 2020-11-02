using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class WalletLog
    {
        public int WalletLogId { get; set; }
        public int RiderId { get; set; }
        public decimal Points { get; set; }
        public decimal CurrentPoints { get; set; }
        public string CurrentStatus { get; set; }
        public DateTime LogDate { get; set; }
    }
}
