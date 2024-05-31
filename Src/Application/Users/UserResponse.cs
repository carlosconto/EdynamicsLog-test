namespace Application.Users;

public record UserResponse(
	int Id,
	string Email,
	int TenantId
	);