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
	public class BookingStatusController : ControllerBase
    {
		private readonly IMediator mediator;

		public BookingStatusController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateBookingStatusDto dto)
		{
			var result = await mediator.Send(new CreateBookingStatusCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateBookingStatusDto dto)
		{
			await mediator.Send(new UpdateBookingStatusCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteBookingStatusCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllBookingStatusDto>>> GetAllBookingStatus()
		{
			var result = await mediator.Send(new GetAllBookingStatusQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetBookingStatusByIdDto>> GetBookingStatusById(int id)
		{
			var result = await mediator.Send(new GetBookingStatusByIdQuery(id));

			return Ok(result);
		}
	}
}
