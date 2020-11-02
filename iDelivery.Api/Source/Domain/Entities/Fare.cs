using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class Fare
    {
        public Fare()
        {
            Bookings = new HashSet<Booking>();
            Customers = new HashSet<Customer>();
        }

        public int FareId { get; set; }
        public decimal BaseFare { get; set; }
        public string TotalBaseKilometers { get; set; }
        public decimal? Surcharge { get; set; }
        public decimal PricePerKilometer { get; set; }
        public string RidersPercentage { get; set; }
        public string CompanyPercentage { get; set; }
        public bool? IsDefault { get; set; }
        public decimal? AllowedBalance { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
