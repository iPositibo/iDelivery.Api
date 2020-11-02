using System;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllBookingHistoriesDto
    {
        public int BookingHistoryId { get; set; }
        public int BookingStatusId { get; set; }
        public string BookingStatusName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
