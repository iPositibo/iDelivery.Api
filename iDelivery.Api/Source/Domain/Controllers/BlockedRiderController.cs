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
	public class BlockedRiderController : ControllerBase
	{
		private readonly IMediator mediator;

		public BlockedRiderController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateBlockedRiderDto dto)
		{
			var result = await mediator.Send(new CreateBlockedRiderCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateBlockedRiderDto dto)
		{
			await mediator.Send(new UpdateBlockedRiderCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteBlockedRiderCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllBlockedRidersDto>>> GetAllBlockedRiders()
		{
			var result = await mediator.Send(new GetAllBlockedRidersQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetBlockedRiderByIdDto>> GetBlockedRiderById(int id)
		{
			var result = await mediator.Send(new GetBlockedRiderByIdQuery(id));

			return Ok(result);
		}
	}
}
