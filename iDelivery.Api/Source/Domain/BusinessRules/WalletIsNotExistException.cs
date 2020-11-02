using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class WalletIsNotExistException : BusinessRulesException
    {
        private const string message = "Wallet is not exist.";

        public WalletIsNotExistException() : base(HttpStatusCode.NotFound, message) { }
    }
}
