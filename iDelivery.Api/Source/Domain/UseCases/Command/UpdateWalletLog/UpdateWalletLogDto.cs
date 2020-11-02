using System;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateWalletLogDto
    {
        public int RiderName { get; set; }
        public decimal Points { get; set; }
        public decimal CurrentPoints { get; set; }
        public string CurrentStatus { get; set; }
        public DateTime LogDate { get; set; }
    }
}
