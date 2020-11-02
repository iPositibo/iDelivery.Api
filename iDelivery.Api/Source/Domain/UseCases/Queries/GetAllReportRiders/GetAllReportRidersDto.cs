using System;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllReportRidersDto
    {
        public int ReportId { get; set; }
        public int CustomerId { get; set; }
        public int RiderId { get; set; }
        public string Comments { get; set; }
        public DateTime DateReported { get; set; }
        public string DateReportedFormatted { get; set; }
    }
}
