﻿namespace Domain.Users;

public interface IJwtService
{
	public string CreateToken(User user);
}