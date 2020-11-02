using System;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllFeedbacksDto
    {
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public DateTime DateReported { get; set; }
        public string DateReportedFormatted { get; set; }
    }
}
