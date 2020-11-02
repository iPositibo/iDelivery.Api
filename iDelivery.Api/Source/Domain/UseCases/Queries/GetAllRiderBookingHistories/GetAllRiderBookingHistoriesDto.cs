namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllRiderBookingHistoriesDto
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
        public string TotalKilometers { get; set; }
        public string RiderShares { get; set; }
        public int BookingStatusId { get; set; }
        public string BookingStatusName { get; set; }
        public string BookingStatusColor { get; set; }
    }
}
