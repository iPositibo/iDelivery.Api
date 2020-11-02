namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllowedLocationByIdDto
    {
        public int AllowedLocationId { get; set; }
        public string Location { get; set; }
        public bool IsAllowed { get; set; }
    }
}
