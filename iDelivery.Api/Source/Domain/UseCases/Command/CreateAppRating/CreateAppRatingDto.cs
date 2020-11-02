using System;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateAppRatingDto
    {
        public int CustomerId { get; set; }
        public string Feedback { get; set; }
        public int Rating { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
