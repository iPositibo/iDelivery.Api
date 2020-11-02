using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class WalletStatus
    {
        public WalletStatus()
        {
            Wallets = new HashSet<Wallet>();
        }

        public int WalletStatusId { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
