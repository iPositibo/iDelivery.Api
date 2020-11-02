namespace iDelivery.Api.Source.Domain.UseCases.Client.Authentication
{
    public class ChangePasswordDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
