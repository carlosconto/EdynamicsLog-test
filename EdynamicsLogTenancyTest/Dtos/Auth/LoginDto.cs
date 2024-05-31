using System.ComponentModel.DataAnnotations;

namespace APIEdynamicsLogTenancyTest.Dtos.Auth;

public record LoginDto(
	[Required(ErrorMessage = "E email es requerido")]
	string Email,
	[Required(ErrorMessage = "La contraseña es requerida")]
	string Password);