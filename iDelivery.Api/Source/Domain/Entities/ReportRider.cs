using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class ReportRider
    {
        public int ReportRiderId { get; set; }
        public int CustomerId { get; set; }
        public int RiderId { get; set; }
        public string Comments { get; set; }
        public DateTime DateReported { get; set; }
    }
}
