using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class EmailIsAlreadyRegisteredException : BusinessRulesException
    {
        private const string message = "Email is already registered";

        public EmailIsAlreadyRegisteredException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
