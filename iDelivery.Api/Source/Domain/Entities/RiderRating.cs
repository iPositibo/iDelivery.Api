using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class RiderRating
    {
        public RiderRating()
        {
            Riders = new HashSet<Rider>();
        }

        public int RiderRatingId { get; set; }
        public int RiderId { get; set; }
        public int? BlockedCount { get; set; }
        public int? ReportedCount { get; set; }
        public int Rating { get; set; }
        public DateTime DateCreated { get; set; }
        public string Feedback { get; set; }

        public virtual ICollection<Rider> Riders { get; set; }
    }
}
