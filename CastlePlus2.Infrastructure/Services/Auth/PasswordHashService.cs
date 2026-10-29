using System;
using System.Security.Cryptography;
using CastlePlus2.Application.Interfaces.Auth;

namespace CastlePlus2.Infrastructure.Services.Auth
{
    public sealed class PasswordHashService : IPasswordHashService
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int DefaultIterations = 600_000;

        public bool Verify(string password, string passwordHash)
        {
            if (password is null || string.IsNullOrWhiteSpace(passwordHash) || passwordHash.Length > 256)
                return false;
            var parts = passwordHash.Split('|');
            if (parts.Length != 4 || parts[0] != "v1" ||
                !int.TryParse(parts[1], out var iterations) || iterations < 1 || iterations > 2_000_000)
                return false;
            try
            {
                var salt = Convert.FromBase64String(parts[2]);
                var storedHash = Convert.FromBase64String(parts[3]);
                if (salt.Length != SaltSize || storedHash.Length != KeySize) return false;
                var computed = HashInternal(password, salt, iterations);
                return CryptographicOperations.FixedTimeEquals(storedHash, computed);
            }
            catch (FormatException) { return false; }

        }
        
        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = HashInternal(password, salt, DefaultIterations);

            return string.Join("|",
                "v1",
                DefaultIterations,
                Convert.ToBase64String(salt),
                Convert.ToBase64String(hash));
        }

        private static byte[] HashInternal(string password, byte[] salt, int iterations)
        {
            using var derive = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            return derive.GetBytes(KeySize);
        }
    }
}
