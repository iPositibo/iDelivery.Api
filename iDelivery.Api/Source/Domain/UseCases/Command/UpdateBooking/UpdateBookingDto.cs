﻿using System;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateBookingDto
    {
        public int BookingStatusId { get; set; }
        public int CustomerId { get; set; }
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
        public string PickupLocation { get; set; }
        public string PickupLongitude { get; set; }
        public string PickupLatitude { get; set; }
        public DateTime? PickupTime { get; set; }
        public string DropOffLocation { get; set; }
        public string DropOffLongitude { get; set; }
        public string DropOffLatitude { get; set; }
        public DateTime? DropOffTime { get; set; }
        public bool? IsActive { get; set; }
    }
}