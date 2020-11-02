using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iDelivery.Api.Source.Domain.UseCases.Command;
using iDelivery.Api.Source.Domain.UseCases.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace iDelivery.Api.Source.Domain.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class AppRatingController : ControllerBase
    {
		private readonly IMediator mediator;

		public AppRatingController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateAppRatingDto dto)
		{
			var result = await mediator.Send(new CreateAppRatingCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateAppRatingDto dto)
		{
			await mediator.Send(new UpdateAppRatingCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteAppRatingCommand(id));
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
