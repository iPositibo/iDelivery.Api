namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllAllowedLocationsDto
    {
        public int AllowedLocationId { get; set; }
        public string Location { get; set; }
        public bool IsAllowed { get; set; }
    }
}
