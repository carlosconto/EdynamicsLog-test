using APIEdynamicsLogTenancyTest.Dtos.Products;
using Application.Products.CreateProduct;
using Infrastructure.Tenat;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIEdynamicsLogTenancyTest.Controllers.Products
{
	[Authorize]
	[Route("{tenant}/[controller]")]
	[ApiController]
	public class CreateProductController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly ITenantProvider _tenantProvider;
		private IHttpContextAccessor _contextAccessor;

		public CreateProductController(IMediator mediator, ITenantProvider tenantProvider, IHttpContextAccessor httpContextAccessor)
		{
			_mediator = mediator;
			_tenantProvider = tenantProvider;
			_contextAccessor = httpContextAccessor;
		}

		[HttpPost]
		public async Task<IActionResult> Store([FromBody] CreateProductDto product)
		{
			try
			{
				await _mediator.Send(new CreateProductCommand(
					_tenantProvider.GetTenantId(),
					product.Name,
					product.Sku,
					10m,
					0,
					_tenantProvider.GetTenantId()
					));

				return Ok(new
				{
					success = true,
					data = "ok",
				});
			}
			catch (Exception ex)
			{
				return BadRequest(new
				{
					succes = false,
					data = ex.Message.ToString(),
				});
			}
		}
	}
}
