using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class CurrentPointsNotEnoughException : BusinessRulesException
    {
        private const string message = "The current points is not enough.";
        public CurrentPointsNotEnoughException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
