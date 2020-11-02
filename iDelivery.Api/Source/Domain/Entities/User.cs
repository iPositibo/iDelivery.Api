using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class User
    {
        public User()
        {
            Customers = new HashSet<Customer>();
            Feedbacks = new HashSet<Feedback>();
            Riders = new HashSet<Rider>();
            UserInRoles = new HashSet<UserInRole>();
            VehicleDetails = new HashSet<VehicleDetail>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Token { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Rider> Riders { get; set; }
        public virtual ICollection<UserInRole> UserInRoles { get; set; }
        public virtual ICollection<VehicleDetail> VehicleDetails { get; set; }
    }
}
