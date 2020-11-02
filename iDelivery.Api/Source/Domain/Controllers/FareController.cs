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
	public class FareController : ControllerBase
    {
		private readonly IMediator mediator;

		public FareController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateFareDto dto)
		{
			var result = await mediator.Send(new CreateFareCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateFareDto dto)
		{
			await mediator.Send(new UpdateFareCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteFareCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllFaresDto>>> GetAllFares()
		{
			var result = await mediator.Send(new GetAllFaresQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetFareByIdDto>> GetFareById(int id)
		{
			var result = await mediator.Send(new GetFareByIdQuery(id));

			return Ok(result);
		}

		[Route("[action]")]
		[HttpGet]
		public async Task<ActionResult<GetDefaultFareDto>> GetDefaultFare()
		{
			var result = await mediator.Send(new GetDefaultFareQuery());

			return Ok(result);
		}
	}
}
