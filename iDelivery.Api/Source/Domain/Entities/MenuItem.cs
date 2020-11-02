using System;
using System.Collections.Generic;

namespace iDelivery.Api.Entities
{
    public partial class MenuItem
    {
        public int MenuItemId { get; set; }
        public string Icon { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
    }
}
