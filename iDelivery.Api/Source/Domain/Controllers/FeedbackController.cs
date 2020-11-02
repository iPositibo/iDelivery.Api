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
    public class FeedbackController : ControllerBase
    {
		private readonly IMediator mediator;

		public FeedbackController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateFeedbackDto dto)
		{
			var result = await mediator.Send(new CreateFeedbackCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateFeedbackDto dto)
		{
			await mediator.Send(new UpdateFeedbackCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteFeedbackCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllFeedbacksDto>>> GetAllFeedbacks()
		{
			var result = await mediator.Send(new GetAllFeedbacksQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetFeedbackByIdDto>> GetFeedbackById(int id)
		{
			var result = await mediator.Send(new GetFeedbackByIdQuery(id));

			return Ok(result);
		}
	}
}
