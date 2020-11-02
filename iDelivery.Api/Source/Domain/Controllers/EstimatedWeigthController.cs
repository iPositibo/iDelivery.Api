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
    public class EstimatedWeigthController : ControllerBase
    {
		private readonly IMediator mediator;

		public EstimatedWeigthController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateEstimatedWeightDto dto)
		{
			var result = await mediator.Send(new CreateEstimatedWeightCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateEstimatedWeightDto dto)
		{
			await mediator.Send(new UpdateEstimatedWeightCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteEstimatedWeightCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllAppRatingsDto>>> GetAllAppRatings()
		{
			var result = await mediator.Send(new GetAllAppRatingsQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetAppRatingByIdDto>> GetAppRatingById(int id)
		{
			var result = await mediator.Send(new GetAppRatingByIdQuery(id));

			return Ok(result);
		}
	}
}
