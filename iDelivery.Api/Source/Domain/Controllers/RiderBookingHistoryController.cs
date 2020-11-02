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
	public class RiderBookingHistoryController : ControllerBase
	{
		private readonly IMediator mediator;

		public RiderBookingHistoryController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateRiderBookingHistoryDto dto)
		{
			var result = await mediator.Send(new CreateRiderBookingHistoryCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateRiderBookingHistoryDto dto)
		{
			await mediator.Send(new UpdateRiderBookingHistoryCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteRiderBookingHistoryCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllRiderBookingHistoriesDto>>> GetAllRiderBookingHistories()
		{
			var result = await mediator.Send(new GetAllRiderBookingHistoriesQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetRiderBookingHistoryByIdDto>> GetRiderBookingHistoryById(int id)
		{
			var result = await mediator.Send(new GetRiderBookingHistoryByIdQuery(id));

			return Ok(result);
		}
	}
}
