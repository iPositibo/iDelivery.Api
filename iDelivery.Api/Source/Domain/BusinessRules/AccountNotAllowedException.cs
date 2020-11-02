using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class AccountNotAllowedException : BusinessRulesException
    {
        private const string message = "The account is not allowed.";
        public AccountNotAllowedException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
