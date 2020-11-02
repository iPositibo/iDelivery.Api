using System;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateCustomerBookingHistoryDto
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
    }
}
