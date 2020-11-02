using System;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class RateAppDto
    {
        public int CustomerId { get; set; }
        public string Feedback { get; set; }
        public int Rating { get; set; }
    }
}
