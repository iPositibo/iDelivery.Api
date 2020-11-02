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
	public partial class UserController : ControllerBase
	{
		private readonly IMediator mediator;

		public UserController(IMediator mediator) => this.mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateUserDto dto)
		{
			var result = await mediator.Send(new CreateUserCommand(dto));

			return Ok(new { id = result });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateUserDto dto)
		{
			await mediator.Send(new UpdateUserCommand(id, dto));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await mediator.Send(new DeleteUserCommand(id));
			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetAllUsersDto>>> GetAllUsers()
		{
			var result = await mediator.Send(new GetAllUsersQuery());

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GetUserByIdDto>> GetUserById(int id)
		{
			var result = await mediator.Send(new GetUserByIdQuery(id));

			return Ok(result);
		}
	}
}
