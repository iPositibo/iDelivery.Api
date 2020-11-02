namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateBookingStatusDto
    {
        public int BookingStatusId { get; set; }
        public string BookingStatusName { get; set; }
        public string StatusColor { get; set; }
    }
}
