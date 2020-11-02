using System;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateBlockedRiderDto
    {
        public int RiderId { get; set; }
        public string Reason { get; set; }
        public DateTime DateBlocked { get; set; }
    }
}
