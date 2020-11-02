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
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class AllowedLocationController : ControllerBase
    {
		private readonly IMediator mediator;

		public AllowedLocationController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateAllowedLocationDto dto)
		{
			var result = await mediator.Send(new CreateAllowedLocationCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateAllowedLocationDto dto)
		{
			await mediator.Send(new UpdateAllowedLocationCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteAllowedLocationCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllAllowedLocationsDto>>> GetAllAlloweLocations()
		{
			var result = await mediator.Send(new GetAllAllowedLocationsQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetAllowedLocationByIdDto>> GetAllowedLocationById(int id)
		{
			var result = await mediator.Send(new GetAllowedLocationByIdQuery(id));

			return Ok(result);
		}
	}
}
