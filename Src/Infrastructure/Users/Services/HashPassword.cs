﻿using Domain.Users;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Users.Services;

public class HashPassword : IHashPassword
{
	private const int KeySize = 64;

	private const int Iterations = 350000;

	private HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

	public string Hash(string password, out byte[] salt)
	{
		salt = RandomNumberGenerator.GetBytes(KeySize);

		var hash = Rfc2898DeriveBytes.Pbkdf2(
			Encoding.UTF8.GetBytes(password),
			salt,
			Iterations,
			_hashAlgorithm,
			KeySize
		);

		return Convert.ToHexString(hash);
	}

	public bool VerifyPassword(string password, string hash, byte[] salt)
	{
		var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithm, KeySize);

		return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
	}
}