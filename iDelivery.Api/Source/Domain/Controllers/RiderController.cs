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
    [ApiController]
	[Route("api/[controller]")]
	[EnableCors("AllowAll")]
	public class RiderController : ControllerBase
    {
		private readonly IMediator mediator;

		public RiderController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateRiderDto dto)
		{
			var result = await mediator.Send(new CreateRiderCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateRiderDto dto)
		{
			await mediator.Send(new UpdateRiderCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteRiderCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllRidersDto>>> GetAllRiders()
		{
			var result = await mediator.Send(new GetAllRidersQuery());

			return Ok(result);
		}

		[Route("[action]")]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllActiveRidersDto>>> GetAllActiveRiders()
		{
			var result = await mediator.Send(new GetAllActiveRidersQuery());

			return Ok(result);
		}

		[Route("[action]")]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllAvailableRiderUsersDto>>> GetAllAvailableRiderUsers()
		{
			var result = await mediator.Send(new GetAllAvailableRiderUsersQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetRiderByIdDto>> GetRiderById(int id)
		{
			var result = await mediator.Send(new GetRiderByIdQuery(id));

			return Ok(result);
		}
	}
}
