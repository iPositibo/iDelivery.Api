using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class UserNotExistException : BusinessRulesException
    {
        private const string message = "User is not exist.";

        public UserNotExistException() : base(HttpStatusCode.NotFound, message) { }
    }
}
