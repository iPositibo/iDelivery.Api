using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class RiderBookingHistory
    {
        public int RiderBookingHistoryId { get; set; }
        public int RiderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverNumber { get; set; }
        public string PickupLocation { get; set; }
        public string DropOffLocation { get; set; }
        public string ItemDetails { get; set; }
        public decimal TotalFare { get; set; }
        public string TotalKilometers { get; set; }
        public string RiderShares { get; set; }
        public int BookingStatusId { get; set; }
        public decimal? RiderFare { get; set; }
        public decimal? RiderDeduction { get; set; }
        public DateTime? BookingDate { get; set; }

        public virtual BookingStatus BookingStatus { get; set; }
    }
}
