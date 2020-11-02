using System;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetFeedbackByIdDto
    {
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime DateReported { get; set; }
    }
}
