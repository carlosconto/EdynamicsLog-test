using APIEdynamicsLogTenancyTest.Dtos.Users;
using Application.Users.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIEdynamicsLogTenancyTest.Controllers.Users
{
	[Authorize]
	[Route("{tenant}/[controller]")]
	[ApiController]
	public class CreateUserController : ControllerBase
	{
		private readonly IMediator _mediator;

		public CreateUserController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> Store([FromBody] CreateUserDto createUser)
		{
			try
			{
				await _mediator.Send(new CreateUserCommand(createUser.Email, createUser.Password));

				return Ok(new
				{
					success = true,
					data = "Ok"
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
