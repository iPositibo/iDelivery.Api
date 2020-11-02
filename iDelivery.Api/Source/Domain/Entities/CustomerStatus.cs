using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class CustomerStatus
    {
        public CustomerStatus()
        {
            Customers = new HashSet<Customer>();
        }

        public int CustomerStatusId { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
