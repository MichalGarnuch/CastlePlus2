using System;

namespace CastlePlus2.Contracts.DTOs.Auth
{
    public sealed class AdminUserDto
    {
        public int IdUzytkownika { get; set; }
        public string Login { get; set; } = string.Empty;
        public string? Email { get; set; }
        public bool CzyAktywny { get; set; }
        public bool CzyUsuniety { get; set; }
        public string[] RoleCodes { get; set; } = Array.Empty<string>();
    }
}