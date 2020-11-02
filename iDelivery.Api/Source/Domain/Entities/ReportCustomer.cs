using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class ReportCustomer
    {
        public int ReportCustomerId { get; set; }
        public int CustomerId { get; set; }
        public int RiderId { get; set; }
        public string Comments { get; set; }
        public DateTime DateReported { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Rider Rider { get; set; }
    }
}
