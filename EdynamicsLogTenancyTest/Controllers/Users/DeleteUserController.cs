using Application.Users.DeleteUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIEdynamicsLogTenancyTest.Controllers.Users
{
	[Authorize]
	[Route("{tenant}/[controller]/{id}")]
	[ApiController]
	public class DeleteUserController : ControllerBase
	{
		private readonly IMediator _mediator;

		public DeleteUserController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpDelete]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			try
			{
				await _mediator.Send(new DeleteUserCommand(id));

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
