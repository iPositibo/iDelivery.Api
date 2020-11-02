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
	public class MenuAccessController : ControllerBase
	{
		private readonly IMediator mediator;

		public MenuAccessController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateMenuAccessDto dto)
		{
			var result = await mediator.Send(new CreateMenuAccessCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateMenuAccessDto dto)
		{
			await mediator.Send(new UpdateMenuAccessCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteMenuAccessCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllMenuAccessDto>>> GetAllMenuAccess()
		{
			var result = await mediator.Send(new GetAllMenuAccessQuery());

			return Ok(result);
		}


		[Route("[action]/{id}")]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetMenuAccessByRoleIdDto>>> GetMenuAccessByRole(int id)
		{
			var result = await mediator.Send(new GetMenuAccessByRoleIdQuery(id));

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetMenuAccessByIdDto>> GetMenuAccessById(int id)
		{
			var result = await mediator.Send(new GetMenuAccessByIdQuery(id));

			return Ok(result);
		}
	}
}
