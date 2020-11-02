using iDelivery.Api.Source.Domain.BusinessRules.Base;
using System.Net;

namespace iDelivery.Api.Source.Domain.BusinessRules
{
    public class RiderVehicleAlreadyRegisteredException : BusinessRulesException
	{
		private const string message = "Rider vehicle is already registered";

		public RiderVehicleAlreadyRegisteredException() : base(HttpStatusCode.BadRequest, message) { }
	}
}
