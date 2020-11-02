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
	public class CustomerBookingHistoryController : ControllerBase
	{
		private readonly IMediator mediator;

		public CustomerBookingHistoryController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateCustomerBookingHistoryDto dto)
		{
			var result = await mediator.Send(new CreateCustomerBookingHistoryCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateCustomerBookingHistoryDto dto)
		{
			await mediator.Send(new UpdateCustomerBookingHistoryCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteCustomerBookingHistoryCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllCustomerBookingHistoriesDto>>> GetAllCustomerBookingHistories()
		{
			var result = await mediator.Send(new GetAllCustomerBookingHistoriesQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetCustomerBookingHistoryByIdDto>> GetCustomerBookingHistoryById(int id)
		{
			var result = await mediator.Send(new GetCustomerBookingHistoryByIdQuery(id));

			return Ok(result);
		}
	}
}
