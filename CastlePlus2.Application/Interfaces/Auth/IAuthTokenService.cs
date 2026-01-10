using System;
using System.Collections.Generic;

namespace CastlePlus2.Application.Interfaces.Auth
{
    public interface IAuthTokenService
    {
        string CreateAccessToken(int userId, string login, IReadOnlyList<string> roleCodes, DateTime utcNow);
        string CreateRefreshToken();
        byte[] HashRefreshToken(string refreshToken);
        DateTime GetAccessTokenExpiresAtUtc(DateTime utcNow);
        DateTime GetRefreshTokenExpiresAtUtc(DateTime utcNow);
    }
}
