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
	public class RiderStatusController : ControllerBase
    {
		private readonly IMediator mediator;

		public RiderStatusController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateRiderStatusDto dto)
		{
			var result = await mediator.Send(new CreateRiderStatusCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateRiderStatusDto dto)
		{
			await mediator.Send(new UpdateRiderStatusCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteRiderStatusCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllRiderStatusDto>>> GetAllRiderStatus()
		{
			var result = await mediator.Send(new GetAllRiderStatusQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetRiderStatusByIdDto>> GetRiderStatusById(int id)
		{
			var result = await mediator.Send(new GetRiderStatusByIdQuery(id));

			return Ok(result);
		}
	}
}
