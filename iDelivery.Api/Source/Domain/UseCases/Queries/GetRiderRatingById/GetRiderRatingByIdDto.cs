using System;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetRiderRatingByIdDto
    {
        public int RiderRatingId { get; set; }
        public int RiderId { get; set; }
        public string RiderName { get; set; }
        public int? BlockedCount { get; set; }
        public int? ReportedCount { get; set; }
        public int Rating { get; set; }
        public DateTime DateCreated { get; set; }
        public string Feedback { get; set; }
    }
}
