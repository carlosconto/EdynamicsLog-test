using System.ComponentModel.DataAnnotations;

namespace APIEdynamicsLogTenancyTest.Dtos.Users;

public record UpdateUserDto(
		[Required(ErrorMessage = "El Email es requerido")]
		string Email,
		string? Password
		);
