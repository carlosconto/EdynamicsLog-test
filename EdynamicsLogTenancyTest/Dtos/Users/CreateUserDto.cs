using System.ComponentModel.DataAnnotations;

namespace APIEdynamicsLogTenancyTest.Dtos.Users;

public record CreateUserDto(
		[Required(ErrorMessage = "El Email es requerido")]
		string Email,
		[Required(ErrorMessage = "La contraseña es requerida")]
		string Password
		);
