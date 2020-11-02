using System;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class RateRiderDto
    {
        public int RiderId { get; set; }
        public int Rating { get; set; }
        public DateTime DateCreated { get; set; }
        public string Feedback { get; set; }
    }
}
