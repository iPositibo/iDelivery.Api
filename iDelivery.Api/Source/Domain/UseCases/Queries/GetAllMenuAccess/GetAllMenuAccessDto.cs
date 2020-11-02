using System;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllMenuAccessDto
    {
        public int MenuAccessId { get; set; }
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime DateCreated { get; set; }
        public string DateCreatedFormatted { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string DateUpdatedFormatted { get; set; }
    }
}
