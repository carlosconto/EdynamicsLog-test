using Application.Products.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIEdynamicsLogTenancyTest.Controllers.Products
{
	[Authorize]
	[Route("{tenant}/[controller]/{id}")]
	[ApiController]
	public class GetProductByIdController : ControllerBase
	{
		private readonly IMediator _mediator;

		public GetProductByIdController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int id)
		{

			try
			{
				var product = await _mediator.Send(new GetProductByIdQuery(id));

				return Ok(new
				{
					success = true,
					data = product
				});
			}
			catch(Exception ex)
			{
				return BadRequest(new
				{
					succes = false,
					data = ex.Message.ToString()
				});
			}
		}
	}
}
