using System;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetCustomerRatingByIdDto
    {
        public int CustomerRatingId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int BlockedCount { get; set; }
        public int ReportedCount { get; set; }
        public int Rating { get; set; }
        public DateTime DateCreated { get; set; }
        public string DateCreatedFormatted { get; set; }
        public string Feedback { get; set; }
    }
}