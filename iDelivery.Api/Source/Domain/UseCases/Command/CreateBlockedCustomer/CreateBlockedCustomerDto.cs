using System;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateBlockedCustomerDto
    {
        public int BlockedCustomerId { get; set; }
        public int CustomerId { get; set; }
        public string Reason { get; set; }
        public DateTime DateBlocked { get; set; }
    }
}
