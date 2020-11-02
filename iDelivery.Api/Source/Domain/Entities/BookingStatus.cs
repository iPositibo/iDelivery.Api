using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class BookingStatus
    {
        public BookingStatus()
        {
            BookingHistories = new HashSet<BookingHistory>();
            Bookings = new HashSet<Booking>();
            CustomerBookingHistories = new HashSet<CustomerBookingHistory>();
            RiderBookingHistories = new HashSet<RiderBookingHistory>();
        }

        public int BookingStatusId { get; set; }
        public string BookingStatusName { get; set; }
        public string StatusColor { get; set; }

        public virtual ICollection<BookingHistory> BookingHistories { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<CustomerBookingHistory> CustomerBookingHistories { get; set; }
        public virtual ICollection<RiderBookingHistory> RiderBookingHistories { get; set; }
    }
}
