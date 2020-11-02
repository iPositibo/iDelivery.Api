using System;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllBlockedRidersDto
    {
        public int BlockedRiderId { get; set; }
        public int RiderId { get; set; }
        public string RiderName { get; set; }
        public string Reason { get; set; }
        public DateTime DateBlocked { get; set; }
        public string DateBlockedFormatted { get; set; }
    }
}
