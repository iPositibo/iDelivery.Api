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
	public class TermsAndConditionController : ControllerBase
    {
		private readonly IMediator mediator;

		public TermsAndConditionController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateTermsAndConditionsDto dto)
		{
			var result = await mediator.Send(new CreateTermsAndConditionsCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateTermsAndConditionsDto dto)
		{
			await mediator.Send(new UpdateTermsAndConditionsCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteTermsAndConditionsCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllTermsAndConditionsDto>>> GetAllTermsAndConditions()
		{
			var result = await mediator.Send(new GetAllTermsAndConditionsQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetTermsAndConditionsByIdDto>> GetTermsAndConditionsById(int id)
		{
			var result = await mediator.Send(new GetTermsAndConditionsByIdQuery(id));

			return Ok(result);
		}
	}
}
