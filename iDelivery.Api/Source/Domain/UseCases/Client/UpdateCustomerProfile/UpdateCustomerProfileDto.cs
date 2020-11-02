namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class UpdateCustomerProfileDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
    }
}
