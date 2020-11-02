using System.ComponentModel.DataAnnotations;

namespace iDelivery.Api.Source.Domain.UseCases.Client.Authentication
{
    public class LoginAdminDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
