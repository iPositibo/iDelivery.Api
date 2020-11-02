namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateFareDto
    {
        public decimal BaseFare { get; set; }
        public string TotalBaseKilometers { get; set; }
        public decimal? Surcharge { get; set; }
        public decimal PricePerKilometer { get; set; }
        public string RidersPercentage { get; set; }
        public string CompanyPercentage { get; set; }
        public bool IsDefault { get; set; }
        public decimal? AllowedBalance { get; set; }
    }
}
