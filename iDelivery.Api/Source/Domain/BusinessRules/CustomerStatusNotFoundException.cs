using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class CustomerStatusNotFoundException : BusinessRulesException
    {
        private const string message = "Customer status not found.";

        public CustomerStatusNotFoundException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
