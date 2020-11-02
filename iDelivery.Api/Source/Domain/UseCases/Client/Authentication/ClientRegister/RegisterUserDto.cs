using System.ComponentModel.DataAnnotations;

namespace iDelivery.Api.Source.Domain.UseCases.Client.Authentication
{
    public class RegisterUserDto
    {
		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		public string Email { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public int? RoleId { get; set; }

        public string PhotoUrl { get; set; }

        public bool? IsCustomer { get; set; }
        public string AccountId { get; set; }
        public bool? IsVerified { get; set; }
    }
}
