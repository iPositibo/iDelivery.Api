using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class AllowedLocation
    {
        public int AllowedLocationId { get; set; }
        public string Location { get; set; }
        public bool IsAllowed { get; set; }
    }
}
