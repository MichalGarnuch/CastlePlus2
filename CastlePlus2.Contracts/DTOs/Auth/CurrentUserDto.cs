using System;

namespace CastlePlus2.Contracts.DTOs.Auth
{
    public class CurrentUserDto
    {
        public int IdUzytkownika { get; set; }
        public string Login { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string[] Role { get; set; } = Array.Empty<string>();
    }
}