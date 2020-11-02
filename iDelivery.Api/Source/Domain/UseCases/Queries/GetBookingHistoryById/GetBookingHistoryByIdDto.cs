using System;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetBookingHistoryByIdDto
    {
        public string BookingStatus { get; set; }
        public int UserId { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
