using System;

namespace CastlePlus2.Contracts.DTOs.Auth
{
    public class AuthTokensDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime AccessTokenExpiresAtUtc { get; set; }
    }
}