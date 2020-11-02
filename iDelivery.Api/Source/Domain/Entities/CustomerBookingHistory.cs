using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class CustomerBookingHistory
    {
        public int CustomerBookingHistoryId { get; set; }
        public int? CustomerId { get; set; }
        public string ReceiverCompleteName { get; set; }
        public string ReceiverCompleteAddress { get; set; }
        public string ItemDetails { get; set; }
        public decimal TotalFare { get; set; }
        public string TotalKilometers { get; set; }
        public string EstimatedTime { get; set; }
        public int BookingStatusId { get; set; }
        public string Receipt { get; set; }
        public DateTime BookingDate { get; set; }

        public virtual BookingStatus BookingStatus { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
