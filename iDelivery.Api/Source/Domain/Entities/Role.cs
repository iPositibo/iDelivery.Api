using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class Role
    {
        public Role()
        {
            MenuAccesses = new HashSet<MenuAccess>();
            UserInRoles = new HashSet<UserInRole>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<MenuAccess> MenuAccesses { get; set; }
        public virtual ICollection<UserInRole> UserInRoles { get; set; }
    }
}
