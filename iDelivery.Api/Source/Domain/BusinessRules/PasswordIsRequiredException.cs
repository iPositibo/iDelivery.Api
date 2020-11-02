using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class PasswordIsRequiredException : BusinessRulesException
    {
        private const string message = "Username Password is required";

        public PasswordIsRequiredException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
