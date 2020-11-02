using System;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateCustomerRatingDto
    {
        public int CustomerRatingId { get; set; }
        public int CustomerId { get; set; }
        public int? BlockedCount { get; set; }
        public int? ReportedCount { get; set; }
        public int Rating { get; set; }
        public DateTime DateCreated { get; set; }
        public string Feedback { get; set; }
    }
}
