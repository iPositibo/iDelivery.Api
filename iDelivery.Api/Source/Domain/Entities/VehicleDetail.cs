using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class VehicleDetail
    {
        public int VehicleDetailId { get; set; }
        public string PlateNumber { get; set; }
        public string Orcr { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public int RiderId { get; set; }

        public virtual User Rider { get; set; }
    }
}
