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
	public class WalletStatusController : ControllerBase
	{
		private readonly IMediator mediator;

		public WalletStatusController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateWalletStatusDto dto)
		{
			var result = await mediator.Send(new CreateWalletStatusCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateWalletStatusDto dto)
		{
			await mediator.Send(new UpdateWalletStatusCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteWalletStatusCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllWalletStatusDto>>> GetAllWalletStatus()
		{
			var result = await mediator.Send(new GetAllWalletStatusQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetWalletStatusByIdDto>> GetWalletStatusById(int id)
		{
			var result = await mediator.Send(new GetWalletStatusByIdQuery(id));

			return Ok(result);
		}
	}
}
