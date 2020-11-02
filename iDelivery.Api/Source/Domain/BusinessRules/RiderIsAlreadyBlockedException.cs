using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class RiderIsAlreadyBlockedException : BusinessRulesException
    {
        private const string message = "Rider is already blocked!";
        public RiderIsAlreadyBlockedException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
