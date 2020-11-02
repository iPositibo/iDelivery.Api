using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class ExternalAccount
    {
        public int ExternalAccountId { get; set; }
        public string Type { get; set; }
        public string AccountId { get; set; }
        public int? UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
