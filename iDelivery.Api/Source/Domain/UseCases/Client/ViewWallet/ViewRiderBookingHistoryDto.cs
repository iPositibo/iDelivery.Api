using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client.ViewWallet
{
    public class ViewRiderBookingHistoryDto
    {
        public int RiderBookingHistoryId { get; set; }
        public int RiderId { get; set; }
        public string RiderName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverNumber { get; set; }
        public string PickupLocation { get; set; }
        public string DropOffLocation { get; set; }
        public string ItemDetails { get; set; }
        public decimal TotalFare { get; set; }
        public string TotalFareFormatted { get; set; }
        public string TotalKilometers { get; set; }
        public string RiderShares { get; set; }
        public int BookingStatusId { get; set; }
        public string BookingStatusName { get; set; }
        public decimal? RiderFare { get; set; }
        public string RiderFareFormatted { get; set; }
        public decimal? RiderDeduction { get; set; }
        public string RiderDeductionFormatted { get; set; }
        public DateTime? BookingDate { get; set; }
        public string BookingDateFormatted { get; set; }
    }
}
