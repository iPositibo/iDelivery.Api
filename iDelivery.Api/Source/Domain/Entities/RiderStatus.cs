using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class RiderStatus
    {
        public RiderStatus()
        {
            Riders = new HashSet<Rider>();
        }

        public int RiderStatusId { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Rider> Riders { get; set; }
    }
}
