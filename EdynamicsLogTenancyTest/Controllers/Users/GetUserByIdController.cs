using Application.Users.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIEdynamicsLogTenancyTest.Controllers.Users
{
	[Authorize]
	[Route("{tenant}/[controller]/{id}")]
	[ApiController]
	public class GetUserByIdController : ControllerBase
	{
		private readonly IMediator _mediator;

		public GetUserByIdController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> Index([FromRoute] int id)
		{

			try
			{
				var response = await _mediator.Send(new GetUserByIdQuery(id));

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
