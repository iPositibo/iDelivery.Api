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
	public class WalletController : ControllerBase
    {
		private readonly IMediator mediator;

		public WalletController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateWalletDto dto)
		{
			var result = await mediator.Send(new CreateWalletCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateWalletDto dto)
		{
			await mediator.Send(new UpdateWalletCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteWalletCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllWalletsDto>>> GetAllWallets()
		{
			var result = await mediator.Send(new GetAllWalletsQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetWalletByIdDto>> GetWalletById(int id)
		{
			var result = await mediator.Send(new GetWalletByIdQuery(id));

			return Ok(result);
		}
	}
}
