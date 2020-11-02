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
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    public class VehicleDetailController : ControllerBase
    {
		private readonly IMediator mediator;

		public VehicleDetailController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateVehicleDetailDto dto)
		{
			var result = await mediator.Send(new CreateVehicleDetailCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateVehicleDetailDto dto)
		{
			await mediator.Send(new UpdateVehicleDetailCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteVehicleDetailCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllVehicleDetailsDto>>> GetAllRiders()
		{
			var result = await mediator.Send(new GetAllVehicleDetailsQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetVehicleDetailByIdDto>> GetVehicleDetailById(int id)
		{
			var result = await mediator.Send(new GetVehicleDetailByIdQuery(id));

			return Ok(result);
		}
	}
}
