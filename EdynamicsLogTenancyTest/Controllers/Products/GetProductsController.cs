using Application.Products.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIEdynamicsLogTenancyTest.Controllers.Products
{
	[Authorize]
	[Route("{tenant}/[controller]")]
	[ApiController]
	public class GetProductsController : ControllerBase
	{
		private readonly IMediator _mediator;
		private IHttpContextAccessor _contextAccessor;

		public GetProductsController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
		{
			_mediator = mediator;
			_contextAccessor = httpContextAccessor;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{

			try
			{
				var products = await _mediator.Send(new GetProductsQuery());
				return Ok(new
				{
					success = true,
					data = products,
					tenan = _contextAccessor.HttpContext.Items.FirstOrDefault()
				});
			}
			catch(Exception ex)
			{
				return Ok(new
				{
					success = false,
					data = ex.Message.ToString()
				});
			}
		}
	}
}
