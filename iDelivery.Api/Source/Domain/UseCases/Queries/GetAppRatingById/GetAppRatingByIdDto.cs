using System;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAppRatingByIdDto
    {
        public int AppRatingId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Feedback { get; set; }
        public int Rating { get; set; }
        public DateTime DateCreated { get; set; }
        public string DateCreatedFormatted { get; set; }
    }
}
