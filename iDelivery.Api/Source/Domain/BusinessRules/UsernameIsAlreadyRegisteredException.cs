using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class UsernameIsAlreadyRegisteredException : BusinessRulesException
	{
		private const string message = "Username is already registered";

		public UsernameIsAlreadyRegisteredException() : base(HttpStatusCode.BadRequest, message) { }
	}
}
