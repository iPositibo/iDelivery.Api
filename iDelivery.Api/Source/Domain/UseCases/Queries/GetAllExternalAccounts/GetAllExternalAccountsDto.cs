using System;

namespace iDelivery.Api.Source.Domain.UseCases.Queries
{
    public class GetAllExternalAccountsDto
    {
        public int ExternalAccountId { get; set; }
        public string Type { get; set; }
        public string AccountId { get; set; }
        public int? UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public string DateCreatedFormatted { get; set; }
    }
}
