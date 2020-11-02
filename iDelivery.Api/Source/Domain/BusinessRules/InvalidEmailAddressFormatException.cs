using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class InvalidEmailAddressFormatException : BusinessRulesException
    {
        private const string message = "Invalid Email Address format";

        public InvalidEmailAddressFormatException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
