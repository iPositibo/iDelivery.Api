using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class OnlyStatusReadyIsAllowedException : BusinessRulesException
    {
        private const string message = "Only the booking with status ready is allowed.";
        public OnlyStatusReadyIsAllowedException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
