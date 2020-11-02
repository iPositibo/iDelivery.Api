namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllAvailableRiderUsersDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string RoleName { get; set; }
    }
}
