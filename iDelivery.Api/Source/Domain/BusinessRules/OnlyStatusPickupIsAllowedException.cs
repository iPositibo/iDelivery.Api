using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class OnlyStatusPickupIsAllowedException : BusinessRulesException
    {
        private const string message = "Only the booking with status pickup is allowed.";
        public OnlyStatusPickupIsAllowedException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
