using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class AccountAlreadyRegisteredException : BusinessRulesException
    {
        private const string message = "The account was already registered.";
        public AccountAlreadyRegisteredException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
