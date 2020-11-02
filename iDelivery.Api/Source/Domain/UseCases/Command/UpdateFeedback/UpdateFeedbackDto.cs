using System;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateFeedbackDto
    {
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
    }
}
