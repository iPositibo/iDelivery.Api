using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class Rating
    {
        public int RatingId { get; set; }
        public int CustomerId { get; set; }
        public int RiderId { get; set; }
        public int Value { get; set; }
    }
}
