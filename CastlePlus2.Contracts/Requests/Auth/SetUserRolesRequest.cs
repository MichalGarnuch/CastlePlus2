using System;

namespace CastlePlus2.Contracts.Requests.Auth
{
    public sealed class SetUserRolesRequest
    {
        public string[] RoleCodes { get; set; } = Array.Empty<string>();
    }
}