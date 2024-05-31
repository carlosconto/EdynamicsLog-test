using Application.Products.DeleteProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIEdynamicsLogTenancyTest.Controllers.Products
{
	[Authorize]
	[Route("{tenant}/[controller]/{id}")]
	[ApiController]
	public class DeleteProductController : ControllerBase
	{
		private readonly IMediator _mediator;

		public DeleteProductController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpDelete]
		public async Task<IActionResult> Delete([FromRoute]int id)
		{

			try
			{
				await _mediator.Send(new DeleteProductCommand(id));

				return Ok(new
				{
					success = true,
					data = "ok"
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
