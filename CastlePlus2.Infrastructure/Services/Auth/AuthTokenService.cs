using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CastlePlus2.Application.Interfaces.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CastlePlus2.Infrastructure.Services.Auth
{
    public sealed class AuthTokenService : IAuthTokenService
    {
        private readonly IConfiguration _configuration;

        public AuthTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateAccessToken(int userId, string login, IReadOnlyList<string> roleCodes, DateTime utcNow)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var signingKey = _configuration["Jwt:SigningKey"];
            if (string.IsNullOrWhiteSpace(signingKey))
                throw new InvalidOperationException("Brak konfiguracji Jwt:SigningKey.");

            var expires = GetAccessTokenExpiresAtUtc(utcNow);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim("login", login)
            };

            foreach (var roleCode in roleCodes)
                claims.Add(new Claim(ClaimTypes.Role, roleCode));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: utcNow,
                expires: expires,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(32);
            return Base64UrlEncoder.Encode(bytes);
        }

        public byte[] HashRefreshToken(string refreshToken)
        {
            var bytes = Encoding.UTF8.GetBytes(refreshToken);
            return SHA256.HashData(bytes); // 32 bytes -> varbinary(32)
        }

        public DateTime GetAccessTokenExpiresAtUtc(DateTime utcNow)
        {
            var minutes = _configuration.GetValue<int>("Jwt:AccessTokenMinutes");
            return utcNow.AddMinutes(minutes);
        }

        public DateTime GetRefreshTokenExpiresAtUtc(DateTime utcNow)
        {
            var days = _configuration.GetValue<int>("Jwt:RefreshTokenDays");
            return utcNow.AddDays(days);
        }
    }
}
