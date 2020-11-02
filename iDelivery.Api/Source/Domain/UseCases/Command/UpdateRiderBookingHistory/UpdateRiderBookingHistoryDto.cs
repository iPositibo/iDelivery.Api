namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateRiderBookingHistoryDto
    {
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
    }
}
