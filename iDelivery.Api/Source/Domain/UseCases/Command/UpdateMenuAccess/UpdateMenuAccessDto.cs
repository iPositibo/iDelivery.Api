using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class UpdateMenuAccessDto
    {
        public int MenuItemId { get; set; }
        public int RoleId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
