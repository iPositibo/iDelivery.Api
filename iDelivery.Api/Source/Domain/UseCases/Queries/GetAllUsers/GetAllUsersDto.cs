namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllUsersDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
