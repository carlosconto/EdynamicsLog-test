using Application.Users.GetUserById;
using Application.Users.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIEdynamicsLogTenancyTest.Controllers.Users
{
	[Authorize]
	[Route("{tenant}/[controller]")]
	[ApiController]
	public class GetUsersController : ControllerBase
	{
		private readonly IMediator _mediator;

		public GetUsersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{

			try
			{
				var response = await _mediator.Send(new GetUsersQuery());

				return Ok(new
				{
					success = true,
					data = response
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
