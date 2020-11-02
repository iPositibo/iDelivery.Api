namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ViewAllowedLocationsDto
    {
        public int AllowedLocationId { get; set; }
        public string Location { get; set; }
        public bool IsAllowed { get; set; }
    }
}
