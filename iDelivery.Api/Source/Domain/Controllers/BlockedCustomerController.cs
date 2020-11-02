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
	public class BlockedCustomerController : ControllerBase
	{
		private readonly IMediator mediator;

		public BlockedCustomerController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateBlockedCustomerDto dto)
		{
			var result = await mediator.Send(new CreateBlockedCustomerCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateBlockedCustomerDto dto)
		{
			await mediator.Send(new UpdateBlockedCustomerCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteBlockedCustomerCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllBlockedCustomersDto>>> GetAllBlockedCustomers()
		{
			var result = await mediator.Send(new GetAllBlockedCustomersQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetBlockedCustomerByIdDto>> GetBlockedCustomerById(int id)
		{
			var result = await mediator.Send(new GetBlockedCustomerByIdQuery(id));

			return Ok(result);
		}
	}
}
