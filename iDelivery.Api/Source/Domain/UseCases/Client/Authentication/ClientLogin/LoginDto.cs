using System.ComponentModel.DataAnnotations;

namespace iDelivery.Api.Source.Domain.UseCases.Client.Authentication
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
