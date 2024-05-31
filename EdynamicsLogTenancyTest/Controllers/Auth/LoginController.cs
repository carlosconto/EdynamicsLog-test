using APIEdynamicsLogTenancyTest.Dtos.Auth;
using Application.Auth.Login;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIEdynamicsLogTenancyTest.Controllers.Auth
{
	[Route("{tenant}/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly IMediator _mediator;

		public LoginController(IMediator mediator)
		{
			_mediator = mediator;
		}


		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginDto login)
		{
			try
			{
				var response = await _mediator.Send(new LoginQuery(login.Email, login.Password));

				return Ok(new
				{
					success = true,
					data = response
				});

			}catch(Exception ex)
			{
				return BadRequest(new
				{
					success = false,
					data = ex.Message.ToString()
				});
			}
		}
	}
}
