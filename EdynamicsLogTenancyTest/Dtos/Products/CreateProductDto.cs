using System.ComponentModel.DataAnnotations;

namespace APIEdynamicsLogTenancyTest.Dtos.Products;

public record CreateProductDto(
	[Required(ErrorMessage = "El nombre es requerido")]
	string Name,
	[Required(ErrorMessage = "El sku es requerido")]
	string Sku,
	[Required(ErrorMessage = "El precio es requerido")]
	string Price
	);