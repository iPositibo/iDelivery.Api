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
	public class RiderRatingController : ControllerBase
	{
		private readonly IMediator mediator;

		public RiderRatingController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateRiderRatingDto dto)
		{
			var result = await mediator.Send(new CreateRiderRatingCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateRiderRatingDto dto)
		{
			await mediator.Send(new UpdateRiderRatingCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteRiderRatingCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllRiderRatingsDto>>> GetAllRiderRatings()
		{
			var result = await mediator.Send(new GetAllRiderRatingsQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetRiderRatingByIdDto>> GetRiderRatingById(int id)
		{
			var result = await mediator.Send(new GetRiderRatingByIdQuery(id));

			return Ok(result);
		}
	}
}
