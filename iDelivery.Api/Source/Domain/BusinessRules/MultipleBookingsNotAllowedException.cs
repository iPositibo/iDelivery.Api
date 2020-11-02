using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class MultipleBookingsNotAllowedException : BusinessRulesException
    {
        private const string message = "Multiple bookings is not allowed.";

        public MultipleBookingsNotAllowedException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
