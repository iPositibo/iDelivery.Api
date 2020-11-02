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
	public class WalletLogController : ControllerBase
    {
		private readonly IMediator mediator;

		public WalletLogController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateWalletLogDto dto)
		{
			var result = await mediator.Send(new CreateWalletLogCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateWalletLogDto dto)
		{
			await mediator.Send(new UpdateWalletLogCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteWalletLogCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllWalletLogsDto>>> GetAllWalletLogs()
		{
			var result = await mediator.Send(new GetAllWalletLogsQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetWalletLogByIdDto>> GetWalletLogById(int id)
		{
			var result = await mediator.Send(new GetWalletLogByIdQuery(id));

			return Ok(result);
		}
	}
}
