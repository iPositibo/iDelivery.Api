using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class AppRating
    {
        public int AppRatingId { get; set; }
        public int CustomerId { get; set; }
        public string Feedback { get; set; }
        public int Rating { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
