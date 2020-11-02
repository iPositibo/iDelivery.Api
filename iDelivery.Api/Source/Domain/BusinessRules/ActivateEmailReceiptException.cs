using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class ActivateEmailReceiptException : BusinessRulesException
    {
        private const string message = "Please activate your email receipt.";
        public ActivateEmailReceiptException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
