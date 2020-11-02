using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class Rate
    {
        public int RateId { get; set; }
        public string Kilometer { get; set; }
        public decimal Fare { get; set; }
    }
}
