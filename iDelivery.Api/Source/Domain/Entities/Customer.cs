using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            AppRatings = new HashSet<AppRating>();
            BlockedCustomers = new HashSet<BlockedCustomer>();
            BookingHistories = new HashSet<BookingHistory>();
            CustomerBookingHistories = new HashSet<CustomerBookingHistory>();
            ReportCustomers = new HashSet<ReportCustomer>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public bool ActivateEmailReceipts { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int? CustomerStatusId { get; set; }
        public int? RatingId { get; set; }
        public int? UserId { get; set; }
        public int? FareId { get; set; }
        public string VerificationCode { get; set; }
        public bool? IsVerified { get; set; }

        public virtual CustomerStatus CustomerStatus { get; set; }
        public virtual Fare Fare { get; set; }
        public virtual CustomerRating Rating { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<AppRating> AppRatings { get; set; }
        public virtual ICollection<BlockedCustomer> BlockedCustomers { get; set; }
        public virtual ICollection<BookingHistory> BookingHistories { get; set; }
        public virtual ICollection<CustomerBookingHistory> CustomerBookingHistories { get; set; }
        public virtual ICollection<ReportCustomer> ReportCustomers { get; set; }
    }
}
