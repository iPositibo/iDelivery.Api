using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class BookingsWithStatusReadyNotFoundException : BusinessRulesException
    {
        private const string message = "There is no bookings with status of ready.";
        public BookingsWithStatusReadyNotFoundException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
