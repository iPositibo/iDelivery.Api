using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public int? UserId { get; set; }
        public string Message { get; set; }
        public DateTime DateReported { get; set; }

        public virtual User User { get; set; }
    }
}
