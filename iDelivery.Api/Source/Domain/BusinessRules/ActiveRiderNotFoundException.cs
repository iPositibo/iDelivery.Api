using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class ActiveRiderNotFoundException : BusinessRulesException
    {
        private const string message = "There is no active riders.";
        public ActiveRiderNotFoundException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
