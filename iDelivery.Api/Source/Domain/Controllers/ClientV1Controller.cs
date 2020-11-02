using iDelivery.Api.Source.Domain.UseCases.Client;
using iDelivery.Api.Source.Domain.UseCases.Client.Authentication;
using iDelivery.Api.Source.Domain.UseCases.Client.SendEmail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class ClientV1Controller : ControllerBase
    {
        private readonly IMediator mediator;

        public ClientV1Controller(IMediator mediator) => this.mediator = mediator;

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> BookDelivery([FromBody] BookDeliveryDto dto)
        {
            var result = await mediator.Send(new BookDelivery(dto));

            return Ok(result);
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> CancelCustomerBooking(int bookingId)
        {
            await mediator.Send(new CancelCustomerBooking(bookingId));

            return Ok();
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> CancelRiderBooking(int bookingId, int riderId)
        {
            await mediator.Send(new CancelRiderBooking(bookingId, riderId));

            return Ok();
        }

        [Route("[action]/{id, points}")]
        [HttpPut]
        public async Task<IActionResult> DeductPoints(int id, int points)
        {
            await mediator.Send(new DeductPoints(id, points));

            return Ok();
        }


        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> AcceptBooking(int bookingId, int riderId)
        {
            var result = await mediator.Send(new AcceptBooking(bookingId, riderId));

            return Ok(result);
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> PickupBooking(int bookingId, int riderId)
        {
            var result = await mediator.Send(new PickupBooking(bookingId, riderId));

            return Ok(result);
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> DropOffBooking(int bookingId, int riderId)
        {
            var result = await mediator.Send(new DropOffBooking(bookingId, riderId));

            return Ok(result);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> RateRider([FromBody] RateRiderDto dto)
        {
            var result = await mediator.Send(new RateRider(dto));

            return Ok(new { id = result });
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> RateCustomer([FromBody] RateCustomerDto dto)
        {
            var result = await mediator.Send(new RateCustomer(dto));

            return Ok(new { id = result });
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> ReceivedEmailReceipts(int id, bool isActiveEmail)
        {
            var result = await mediator.Send(new ReceivedEmailReceipts(id, isActiveEmail));

            return Ok(result);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewAllBookingsDto>>> ViewAllBookings()
        {
            var result = await mediator.Send(new ViewAllBookings());

            return Ok(result);
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewCustomerBookingsDto>>> ViewCustomerBookings(int id)
        {
            var result = await mediator.Send(new ViewCustomerBookings(id));

            return Ok(result);
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewRiderBookingsDto>>> ViewRiderBookings(int id)
        {
            var result = await mediator.Send(new ViewRiderBookings(id));

            return Ok(result);
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult<ViewRiderProfileDto>> ViewRiderProfile(int id)
        {
            var result = await mediator.Send(new ViewRiderProfile(id));

            return Ok(result);
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> UpdateRiderProfile(UpdateRiderProfileDto riderProfile)
        {
            var result = await mediator.Send(new UpdateRiderProfileCommand(riderProfile));

            return Ok(result);
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult<ViewCustomerProfileDto>> ViewCustomerProfile(int id)
        {
            var result = await mediator.Send(new ViewCustomerProfile(id));

            return Ok(result);
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> UpdateCustomerProfile(UpdateCustomerProfileDto customerProfile)
        {
            var result = await mediator.Send(new UpdateCustomerProfileCommand(customerProfile));

            return Ok(result);
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult<ViewWalletDto>> ViewWallet(int id)
        {
           var result = await mediator.Send(new ViewWalletCommand(id));

            return Ok(result);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> ReportCustomer([FromBody] ReportCustomerDto dto)
        {
            var result = await mediator.Send(new ReportCustomerCommand(dto));

            return Ok(new { id = result });
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> ReportRider([FromBody] ReportRiderDto dto)
        {
            var result = await mediator.Send(new ReportRiderCommand(dto));

            return Ok(new { id = result });
        }

        [Route("[action]/{id, isActive}")]
        [HttpPut]
        public async Task<IActionResult> ActivateDeactivateBooking(int id, bool isActive)
        {
            await mediator.Send(new ActivateDeactivateBooking(id, isActive));

            return Ok();
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> RegisterNumber([FromBody] RegisterNumberDto dto)
        {
            var result = await mediator.Send(new RegisterNumber(dto));

            return Ok(new { id = result });
        }

        [Route("[action]/{kilometers}")]
        [HttpGet]
        public async Task<ActionResult<decimal>> ComputeFare(decimal kilometers)
        {
            var result = await mediator.Send(new ComputeFare(kilometers));

            return Ok(result);
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> SetRiderLocation(int id, string longitude, string latitude)
        {
            await mediator.Send(new SetRiderLocationCommand(id, longitude, latitude));

            return Ok();
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> SetCustomerLocation(int id, string longitude, string latitude)
        {
            await mediator.Send(new SetCustomerLocationCommand(id, longitude, latitude));

            return Ok();
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> RateApp([FromBody] RateAppDto dto)
        {
            var result = await mediator.Send(new RateApp(dto));

            return Ok(result);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> SendEmailReceipt(string email)
        {
            var result = await mediator.Send(new SendEmailReceipt(email));

            return Ok(result);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> SendEmailVerification(string email)
        {
            var result = await mediator.Send(new SendEmailVerificationCommand(email));

            return Ok(result);
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> SetReadyBooking(int bookingId)
        {
            await mediator.Send(new SetReadyBookingCommand(bookingId));

            return Ok();
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> SetOnlineRider(int riderId, bool isOnline)
        {
            await mediator.Send(new SetOnlineRiderCommand(riderId, isOnline));

            return Ok();
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult<ViewOnlineRidersDto>> ViewOnlineRider(int id)
        {
            var result = await mediator.Send(new ViewOnlineRidersQuery(id));

            return Ok(result);
        }
    }
}
