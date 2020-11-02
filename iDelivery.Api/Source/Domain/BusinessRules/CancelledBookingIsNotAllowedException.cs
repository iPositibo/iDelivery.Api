using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class CancelledBookingIsNotAllowedException : BusinessRulesException
    {
        private const string message = "Cancelled booking is not allowed.";

        public CancelledBookingIsNotAllowedException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
