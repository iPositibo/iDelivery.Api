using System;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetMenuAccessByRoleIdDto
    {
        public int MenuAccessId { get; set; }
        public int MenuItemId { get; set; }
        public int RoleId { get; set; }
        public DateTime DateCreated { get; set; }
        public string DateCreatedFormatted { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string DateUpdatedFormatted { get; set; }
    }
}
