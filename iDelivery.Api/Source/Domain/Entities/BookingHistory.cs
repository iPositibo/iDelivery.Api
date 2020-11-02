using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class BookingHistory
    {
        public int BookingHistoryId { get; set; }
        public int BookingStatusId { get; set; }
        public int CustomerId { get; set; }
        public DateTime BookingDate { get; set; }

        public virtual BookingStatus BookingStatus { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
