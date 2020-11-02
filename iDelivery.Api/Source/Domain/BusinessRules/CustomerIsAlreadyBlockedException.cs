using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class CustomerIsAlreadyBlockedException : BusinessRulesException
    {
        private const string message = "Customer is already blocked!";
        public CustomerIsAlreadyBlockedException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
