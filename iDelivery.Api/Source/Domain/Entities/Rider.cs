using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class Rider
    {
        public Rider()
        {
            BlockedRiders = new HashSet<BlockedRider>();
            ReportCustomers = new HashSet<ReportCustomer>();
            Wallets = new HashSet<Wallet>();
        }

        public int RiderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int? RiderStatusId { get; set; }
        public int? RatingId { get; set; }
        public int? UserId { get; set; }
        public int? TotalCancelledBooking { get; set; }
        public bool ActivateEmailReceipts { get; set; }
        public bool IsOnline { get; set; }

        public virtual RiderRating Rating { get; set; }
        public virtual RiderStatus RiderStatus { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<BlockedRider> BlockedRiders { get; set; }
        public virtual ICollection<ReportCustomer> ReportCustomers { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
