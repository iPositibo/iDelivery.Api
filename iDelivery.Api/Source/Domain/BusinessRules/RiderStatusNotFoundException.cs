using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class RiderStatusNotFoundException : BusinessRulesException
    {
        private const string message = "Rider status not found.";

        public RiderStatusNotFoundException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
