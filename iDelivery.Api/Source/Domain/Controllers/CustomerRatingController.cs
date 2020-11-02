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
	public class CustomerRatingController : ControllerBase
	{
		private readonly IMediator mediator;

		public CustomerRatingController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateCustomerRatingDto dto)
		{
			var result = await mediator.Send(new CreateCustomerRatingCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateCustomerRatingDto dto)
		{
			await mediator.Send(new UpdateCustomerRatingCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteCustomerRatingCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllCustomerRatingsDto>>> GetAllCustomerRatings()
		{
			var result = await mediator.Send(new GetAllCustomerRatingsQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetCustomerRatingByIdDto>> GetCustomerRatingById(int id)
		{
			var result = await mediator.Send(new GetCustomerRatingByIdQuery(id));

			return Ok(result);
		}
	}
}
