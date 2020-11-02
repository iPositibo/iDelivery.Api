using System;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateBlockedCustomerDto
    {
        public int CustomerId { get; set; }
        public string Reason { get; set; }
        public DateTime DateBlocked { get; set; }
    }
}
