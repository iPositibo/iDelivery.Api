using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class FareIsCurrentlyUsedByCustomersException : BusinessRulesException
    {
        private const string message = "The Fare is currently used by the customers.";

        public FareIsCurrentlyUsedByCustomersException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
