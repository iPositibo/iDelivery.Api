using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class OnlyStatusToShipIsAllowedException : BusinessRulesException
    {
        private const string message = "Only the booking with status toship is allowed.";
        public OnlyStatusToShipIsAllowedException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
