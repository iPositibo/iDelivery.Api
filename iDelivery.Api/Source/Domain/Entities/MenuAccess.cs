using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class MenuAccess
    {
        public int MenuAccessId { get; set; }
        public int MenuItemId { get; set; }
        public int RoleId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        public virtual Role Role { get; set; }
    }
}
