using iDelivery.Api.Source.Domain.UseCases.Command;
using iDelivery.Api.Source.Domain.UseCases.Queries;
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
	public class BookingController : ControllerBase 
    {
		private readonly IMediator mediator;

		public BookingController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateBookingDto dto)
		{
			var result = await mediator.Send(new CreateBookingCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateBookingDto dto)
		{
			await mediator.Send(new UpdateBookingCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteBookingCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllBookingsDto>>> GetAllBookings()
		{
			var result = await mediator.Send(new GetAllBookingsQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetBookingByIdDto>> GetBookingById(int id)
		{
			var result = await mediator.Send(new GetBookingByIdQuery(id));

			return Ok(result);
		}

		[Route("[action]")]
		[HttpGet]
		public async Task<ActionResult<GetAllActiveTransactionsDto>> GetAllActiveTransactions()
		{
			var result = await mediator.Send(new GetAllActiveTransactionsQuery());

			return Ok(result);
		}
	}
}
