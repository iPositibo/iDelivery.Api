namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateUserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
