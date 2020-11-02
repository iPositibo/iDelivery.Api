using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class UsernameIsAlreadyTakenException : BusinessRulesException
	{
		private const string message = "Username is already taken";

		public UsernameIsAlreadyTakenException() : base(HttpStatusCode.BadRequest, message) { }
	}
}
