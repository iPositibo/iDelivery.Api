using iDelivery.Api.Source.Domain.UseCases.Client;
using iDelivery.Api.Source.Domain.UseCases.Client.Authentication;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace iDelivery.Api.Source.Domain.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
    [EnableCors("AllowAll")]
    public class AuthenticationController : ControllerBase
    {
		private readonly IMediator mediator;

		public AuthenticationController(IMediator mediator) => this.mediator = mediator;

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] LoginDto dto)
		{
			var loginResult = await mediator.Send(new Login(dto));

			return Ok(loginResult);
		}

		[HttpPost("AdminLogin")]
		public async Task<IActionResult> AdminLogin([FromBody] LoginAdminDto dto)
		{
			var loginResult = await mediator.Send(new LoginAdminCommand(dto));

			return Ok(loginResult);
		}

		[HttpPost("Register")]
		public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto)
		{
			await mediator.Send(new RegisterUser(dto));

			return Ok();
		}

		[HttpPost("AdminRegister")]
		public async Task<IActionResult> AdminRegister([FromBody] RegisterAdminDto dto)
		{
			await mediator.Send(new RegisterAdminCommand(dto));

			return Ok();
		}

		[Route("[action]")]
		[HttpGet]
		public async Task<IActionResult> CheckExternalAccount(string accountId)
		{
			var result = await mediator.Send(new CheckExternalAccountCommand(accountId));

			return Ok(result);
		}

		[Route("[action]")]
		[HttpPut]
		public async Task<IActionResult> ChangePassword(int userId, string password)
		{
			var result = await mediator.Send(new ChangePasswordCommand(userId, password));

			return Ok(result);
		}

		[Route("[action]")]
		[HttpPost]
		public async Task<ActionResult<VerifyCodeDto>> VerifyCode(string code)
		{
			var result = await mediator.Send(new VerifyCode(code));

			return Ok(result);
		}

		[Route("[action]")]
		[HttpPost]
		public async Task<IActionResult> VerifyToken(string token)
		{
			var result = await mediator.Send(new VerifyTokenCommand(token));

			return Ok(result);
		}
	}
}
