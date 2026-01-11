using System;

namespace CastlePlus2.Client.Services.Auth.Storage;

public class AccessTokenPair
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime AccessTokenExpiresAtUtc { get; set; }
}