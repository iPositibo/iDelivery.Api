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
	public class MenuItemController : ControllerBase
	{
		private readonly IMediator mediator;

		public MenuItemController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateMenuItemDto dto)
		{
			var result = await mediator.Send(new CreateMenuItemCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateMenuItemDto dto)
		{
			await mediator.Send(new UpdateMenuItemCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteMenuItemCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllMenuItemsDto>>> GetAllMenuItems()
		{
			var result = await mediator.Send(new GetAllMenuItemsQuery());

			return Ok(result);
		}

		[Route("[action]/{id}")]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetMenuItemsByRoleIdDto>>> GetMenuItemsByRole(int id)
		{
			var result = await mediator.Send(new GetMenuItemsByRoleIdQuery(id));

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetMenuItemByIdDto>> GetMenuItemById(int id)
		{
			var result = await mediator.Send(new GetMenuItemByIdQuery(id));

			return Ok(result);
		}
	}
}
