using System;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllBlockedCustomersDto
    {
        public int BlockedCustomerId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Reason { get; set; }
        public DateTime DateBlocked { get; set; }
        public string DateBlockedFormatted { get; set; }
    }
}
