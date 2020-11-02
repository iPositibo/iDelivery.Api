using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class BlockedCustomer
    {
        public int BlockedCustomerId { get; set; }
        public int CustomerId { get; set; }
        public string Reason { get; set; }
        public DateTime DateBlocked { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
