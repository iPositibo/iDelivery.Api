using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetMenuItemsByRoleIdDto
    {
        public int MenuItemId { get; set; }
        public string Icon { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
    }
}
