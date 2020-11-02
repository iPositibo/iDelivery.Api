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
	public class CustomerController : ControllerBase
    {
		private readonly IMediator mediator;

		public CustomerController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateCustomerDto dto)
		{
			var result = await mediator.Send(new CreateCustomerCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateCustomerDto dto)
		{
			await mediator.Send(new UpdateCustomerCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteCustomerCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllCustomersDto>>> GetAllCustomers()
		{
			var result = await mediator.Send(new GetAllCustomersQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetCustomerByIdDto>> GetCustomerById(int id)
		{
			var result = await mediator.Send(new GetCustomerByIdQuery(id));

			return Ok(result);
		}

		[Route("[action]")]
		[HttpGet]
		public async Task<ActionResult<decimal>> GetAllActiveCustomers()
		{
			var result = await mediator.Send(new GetAllActiveCustomersQuery());

			return Ok(result);
		}

		[Route("[action]")]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllAvailableCustomerUsersDto>>> GetAllAvailableCustomerUsers()
		{
			var result = await mediator.Send(new GetAllAvailableCustomerUsersQuery());

			return Ok(result);
		}

	}
}
