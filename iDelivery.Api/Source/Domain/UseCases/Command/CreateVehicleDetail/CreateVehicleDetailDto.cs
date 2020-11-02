namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateVehicleDetailDto
    {
        public int VehicleDetailId { get; set; }
        public string PlateNumber { get; set; }
        public string Orcr { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public int RiderId { get; set; }
    }
}
