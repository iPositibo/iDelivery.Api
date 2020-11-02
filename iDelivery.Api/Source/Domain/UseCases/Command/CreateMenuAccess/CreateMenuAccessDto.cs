using System;

namespace iDelivery.Api.Source.Domain.UseCases.Command
{
    public class CreateMenuAccessDto
    {
        public int MenuAccessId { get; set; }
        public int MenuItemId { get; set; }
        public int RoleId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
