using System;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class AcceptBookingDto
    {
        public int BookingId { get; set; }
        public int BookingStatusId { get; set; }
        public string BookingStatusName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ContactName { get; set; }
        public string ContactNumber { get; set; }
        public string Items { get; set; }
        public string PhotoUrl { get; set; }
        public string TotalEstimatedWeight { get; set; }
        public string TotalKilometers { get; set; }
        public string EstimatedTime { get; set; }
        public string Notes { get; set; }
        public int? FareId { get; set; }
        public DateTime BookingDate { get; set; }
        public int? RiderId { get; set; }
        public string RiderName { get; set; }
        public string PickupLocation { get; set; }
        public string PickupLongitude { get; set; }
        public string PickupLatitude { get; set; }
        public DateTime? PickupTime { get; set; }
        public string DropOffLocation { get; set; }
        public string DropOffLongitude { get; set; }
        public string DropOffLatitude { get; set; }
        public DateTime? DropOffTime { get; set; }
        public bool? IsActive { get; set; }
        public string ReferenceNumber { get; set; }
        public decimal? TotalFare { get; set; }
    }
}
