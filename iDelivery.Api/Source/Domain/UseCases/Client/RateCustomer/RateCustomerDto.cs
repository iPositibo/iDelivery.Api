using System;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class RateCustomerDto
    {
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public DateTime DateCreated { get; set; }
        public string Feedback { get; set; }
    }
}
