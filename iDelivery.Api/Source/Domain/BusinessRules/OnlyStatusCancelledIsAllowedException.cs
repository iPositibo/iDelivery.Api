using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class OnlyStatusCancelledIsAllowedException : BusinessRulesException
    {
        private const string message = "Only the booking with status cancelled is allowed.";
        public OnlyStatusCancelledIsAllowedException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
