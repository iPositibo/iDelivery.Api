using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class CustomerFare
    {
        public int CustomerFareId { get; set; }
        public int CustomerId { get; set; }
        public int FareId { get; set; }
    }
}
