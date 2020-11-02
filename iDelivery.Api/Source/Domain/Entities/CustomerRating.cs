using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class CustomerRating
    {
        public CustomerRating()
        {
            Customers = new HashSet<Customer>();
        }

        public int CustomerRatingId { get; set; }
        public int CustomerId { get; set; }
        public int? BlockedCount { get; set; }
        public int? ReportedCount { get; set; }
        public int Rating { get; set; }
        public DateTime DateCreated { get; set; }
        public string Feedback { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
