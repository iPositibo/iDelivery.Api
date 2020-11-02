using iDelivery.Api.Source.Domain.UseCases.Command;
using iDelivery.Api.Source.Domain.UseCases.Queries;
using iDelivery.Api.Source.Domain.UseCases.Queries.GetAllFAQs;
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
    public class FAQController : ControllerBase
    {
		private readonly IMediator mediator;

		public FAQController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateFAQDto dto)
		{
			var result = await mediator.Send(new CreateFAQCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateFAQDto dto)
		{
			await mediator.Send(new UpdateFAQCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteFaqCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllFAQsDto>>> GetAllFaqs()
		{
			var result = await mediator.Send(new GetAllFAQsQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetFAQByIdDto>> GetFaqById(int id)
		{
			var result = await mediator.Send(new GetFAQByIdQuery(id));

			return Ok(result);
		}
	}
}
