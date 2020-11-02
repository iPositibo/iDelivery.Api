using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class DefaultFareAlreadyExistException : BusinessRulesException
    {
        private const string message = "There is default fare already exist";
        public DefaultFareAlreadyExistException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
