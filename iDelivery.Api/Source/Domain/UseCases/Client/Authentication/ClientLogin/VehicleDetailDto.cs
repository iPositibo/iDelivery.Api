using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Client.Authentication
{
    public class VehicleDetailDto
    {
        public int VehicleDetailId { get; set; }
        public string PlateNumber { get; set; }
        public string Orcr { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public int RiderId { get; set; }
    }
}
