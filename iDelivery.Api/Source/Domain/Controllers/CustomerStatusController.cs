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
	public class CustomerStatusController : ControllerBase
    {
		private readonly IMediator mediator;

		public CustomerStatusController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateCustomerStatusDto dto)
		{
			var result = await mediator.Send(new CreateCustomerStatusCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateCustomerStatusDto dto)
		{
			await mediator.Send(new UpdateCustomerStatusCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteCustomerStatusCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllCustomerStatusDto>>> GetAllCustomerStatus()
		{
			var result = await mediator.Send(new GetAllCutomerStatusQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetCustomerStatusByIdDto>> GetCustomerStatusById(int id)
		{
			var result = await mediator.Send(new GetCustomerStatusByIdQuery(id));

			return Ok(result);
		}
	}
}
