using APIEdynamicsLogTenancyTest.Dtos.Users;
using Application.Users.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIEdynamicsLogTenancyTest.Controllers.Users
{
	[Authorize]
	[Route("{tenant}/[controller]/{id}")]
	[ApiController]
	public class UpdateUseController : ControllerBase
	{
		private readonly IMediator _mediator;

		public UpdateUseController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateUserDto updateUser, [FromRoute] int id)
		{
			try
			{
				 await _mediator.Send(new UpdateUserCommand(id, updateUser.Email, updateUser.Password));

				return Ok(new
				{
					success = true,
					data = "Ok"
				});
			}
			catch (Exception ex)
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
