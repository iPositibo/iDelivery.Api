using System;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateFeedbackDto
    {
        public int UserId { get; set; }
        public string Message { get; set; }
    }
}
