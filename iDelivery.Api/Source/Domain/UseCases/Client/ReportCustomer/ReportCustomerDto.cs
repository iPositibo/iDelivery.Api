using System;

namespace iDelivery.Api.Source.Domain.UseCases.Client
{
    public class ReportCustomerDto
    {
        public int CustomerId { get; set; }
        public int RiderId { get; set; }
        public string Comments { get; set; }
        public DateTime DateReported { get; set; }
    }
}
