using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class BlockedRider
    {
        public int BlockedRiderId { get; set; }
        public int RiderId { get; set; }
        public string Reason { get; set; }
        public DateTime DateBlocked { get; set; }

        public virtual Rider Rider { get; set; }
    }
}
