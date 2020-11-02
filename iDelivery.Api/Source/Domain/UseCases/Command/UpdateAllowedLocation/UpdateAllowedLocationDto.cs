namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateAllowedLocationDto
    {
        public string Location { get; set; }
        public bool IsAllowed { get; set; }
    }
}
