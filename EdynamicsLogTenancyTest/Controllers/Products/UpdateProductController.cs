using APIEdynamicsLogTenancyTest.Dtos.Products;
using Application.Products.UpdateProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIEdynamicsLogTenancyTest.Controllers.Products
{
	[Authorize]
	[Route("{tenant}/[controller]/{id}")]
	[ApiController]
	public class UpdateProductController : ControllerBase
	{
		private readonly IMediator _mediator;

		public UpdateProductController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateProductDto product, int id)
		{
			try
			{
				await _mediator.Send(new UpdateProductCommand(
					id,
					product.Name,
					product.Sku,
					decimal.Parse(product.Price)
					));

				return Ok(new
				{
					success = true,
					data = "ok"
				});
			}
			catch(Exception ex)
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
