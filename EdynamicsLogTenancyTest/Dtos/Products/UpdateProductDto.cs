using System.ComponentModel.DataAnnotations;

namespace APIEdynamicsLogTenancyTest.Dtos.Products;

public record UpdateProductDto(
	[Required(ErrorMessage = "El nombre es requerido")]
	string Name,
	[Required(ErrorMessage = "El Sku es requerido")]
	string Sku,
	[Required(ErrorMessage = "El Precio es requerido")]
	string Price
	);