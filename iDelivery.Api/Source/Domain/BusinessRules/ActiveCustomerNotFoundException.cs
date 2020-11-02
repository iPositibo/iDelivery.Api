using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class ActiveCustomerNotFoundException : BusinessRulesException
    {
        private const string message = "There is no active customers.";
        public ActiveCustomerNotFoundException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
