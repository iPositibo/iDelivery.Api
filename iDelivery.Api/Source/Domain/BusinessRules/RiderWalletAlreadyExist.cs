using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class RiderWalletAlreadyExist : BusinessRulesException
    {
        private const string message = "Wallet of the Rider is already exist.";

        public RiderWalletAlreadyExist() : base(HttpStatusCode.BadRequest, message) { }
    }
}
