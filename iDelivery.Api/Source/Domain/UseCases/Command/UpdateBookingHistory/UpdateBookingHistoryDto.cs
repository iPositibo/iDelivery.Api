using System;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateBookingHistoryDto
    {
        public int BookingStatusId { get; set; }
        public int CustomerId { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
