using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class NotFoundException : BusinessRulesException
    {
        private const string message = "Record not found.";

        public NotFoundException() : base(HttpStatusCode.NotFound, message) { }
    }
}
