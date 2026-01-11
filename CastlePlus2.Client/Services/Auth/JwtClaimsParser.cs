using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace CastlePlus2.Client.Services.Auth;

public static class JwtClaimsParser
{
    public static IReadOnlyList<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        if (string.IsNullOrWhiteSpace(jwt))
        {
            return claims;
        }

        var parts = jwt.Split('.');
        if (parts.Length < 2)
        {
            return claims;
        }

        var payloadJson = DecodeBase64Url(parts[1]);
        using var payload = JsonDocument.Parse(payloadJson);

        foreach (var property in payload.RootElement.EnumerateObject())
        {
            if (property.Value.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in property.Value.EnumerateArray())
                {
                    var value = item.ToString();
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        claims.Add(new Claim(property.Name, value));
                        if (IsRoleClaim(property.Name))
                        {
                            claims.Add(new Claim(ClaimTypes.Role, value));
                        }
                    }
                }

                continue;
            }

            var stringValue = property.Value.ToString();
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                continue;
            }

            claims.Add(new Claim(property.Name, stringValue));
            if (IsRoleClaim(property.Name))
            {
                claims.Add(new Claim(ClaimTypes.Role, stringValue));
            }
        }

        return claims;
    }

    private static bool IsRoleClaim(string claimType)
    {
        return string.Equals(claimType, "role", StringComparison.OrdinalIgnoreCase)
               || string.Equals(claimType, "roles", StringComparison.OrdinalIgnoreCase)
               || string.Equals(claimType, ClaimTypes.Role, StringComparison.OrdinalIgnoreCase);
    }

    private static string DecodeBase64Url(string base64Url)
    {
        var padded = base64Url.Replace('-', '+').Replace('_', '/');
        switch (padded.Length % 4)
        {
            case 2:
                padded += "==";
                break;
            case 3:
                padded += "=";
                break;
        }

        var bytes = Convert.FromBase64String(padded);
        return Encoding.UTF8.GetString(bytes);
    }
}