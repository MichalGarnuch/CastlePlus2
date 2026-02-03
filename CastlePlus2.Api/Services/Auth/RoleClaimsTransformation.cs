using System.Security.Claims;
using CastlePlus2.Shared.Auth;
using Microsoft.AspNetCore.Authentication;

namespace CastlePlus2.Api.Services.Auth;

public sealed class RoleClaimsTransformation : IClaimsTransformation
{
    private static readonly StringComparer RoleComparer = StringComparer.OrdinalIgnoreCase;
    private static readonly Dictionary<string, string> RoleNormalizationMap = new(RoleComparer)
    {
        [RoleCodes.Admin] = RoleCodes.Admin,
        [RoleCodes.Employee] = RoleCodes.Employee,
        [RoleCodes.Manager] = RoleCodes.Manager,
        [RoleCodes.User] = RoleCodes.User
    };

    private static readonly string[] RoleClaimTypes =
    {
        ClaimTypes.Role,
        "role",
        "roles",
        "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
    };

    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.Identity is not ClaimsIdentity identity || !identity.IsAuthenticated)
        {
            return Task.FromResult(principal);
        }

        var normalizedRoles = new HashSet<string>(RoleComparer);
        foreach (var claim in principal.Claims.Where(c => RoleClaimTypes.Contains(c.Type, StringComparer.OrdinalIgnoreCase)))
        {
            foreach (var role in SplitRoles(claim.Value))
            {
                if (RoleNormalizationMap.TryGetValue(role, out var normalized))
                {
                    normalizedRoles.Add(normalized);
                }
                else if (!string.IsNullOrWhiteSpace(role))
                {
                    normalizedRoles.Add(role.Trim());
                }
            }
        }

        if (normalizedRoles.Count == 0)
        {
            return Task.FromResult(principal);
        }

        var clonedIdentities = principal.Identities.Select(existingIdentity =>
        {
            if (existingIdentity != identity)
            {
                return existingIdentity;
            }

            var newIdentity = new ClaimsIdentity(
                existingIdentity.Claims.Where(c => !IsRoleClaim(c)),
                existingIdentity.AuthenticationType,
                existingIdentity.NameClaimType,
                ClaimTypes.Role);

            foreach (var role in normalizedRoles)
            {
                newIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return newIdentity;
        }).ToList();

        return Task.FromResult(new ClaimsPrincipal(clonedIdentities));
    }

    private static bool IsRoleClaim(Claim claim)
        => RoleClaimTypes.Contains(claim.Type, StringComparer.OrdinalIgnoreCase);

    private static IEnumerable<string> SplitRoles(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            yield break;
        }

        foreach (var role in value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            if (!string.IsNullOrWhiteSpace(role))
            {
                yield return role;
            }
        }
    }
}