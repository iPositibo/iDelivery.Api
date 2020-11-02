using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class RiderIsNotAllowedException : BusinessRulesException
    {
        private const string message = "Rider is not allowed to process this.";
        public RiderIsNotAllowedException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
