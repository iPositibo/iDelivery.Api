using System.Collections.Generic;

namespace iDelivery.Api.Source.Domain.UseCases.Client.Authentication
{
    public class LoginResultDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhotoUrl { get; set; }
        public string Token { get; set; }
        public bool IsActiveEmailReceipt { get; set; }
        public bool IsVerified { get; set; }

        public IEnumerable<RoleDto> Roles { get; set; }
        public VehicleDetailDto VehicleDetail { get; set; }
    }
}
