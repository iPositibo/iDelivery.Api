﻿using System;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllWalletLogsDto
    {
        public int WalletLogId { get; set; }
        public int RiderId { get; set; }
        public string RiderName { get; set; }
        public decimal Points { get; set; }
        public decimal CurrentPoints { get; set; }
        public string CurrentStatus { get; set; }
        public DateTime LogDate { get; set; }
        public string LogDateFormatted { get; set; }
        //public decimal RiderFare { get; set; }
        //public decimal RiderDeduction { get; set; }
    }
}
