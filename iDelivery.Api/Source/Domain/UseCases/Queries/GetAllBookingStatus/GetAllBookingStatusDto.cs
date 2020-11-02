namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllBookingStatusDto
    {
        public int BookingStatusId { get; set; }
        public string BookingStatusName { get; set; }
        public string StatusColor { get; set; }
    }
}
