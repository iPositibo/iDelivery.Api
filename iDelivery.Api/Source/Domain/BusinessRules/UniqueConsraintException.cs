using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class UniqueConsraintException : BusinessRulesException
    {
        private const string message = "Unique constraint.";

        public UniqueConsraintException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
