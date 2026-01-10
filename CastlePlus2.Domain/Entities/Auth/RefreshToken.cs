using System;

namespace CastlePlus2.Domain.Entities.Auth
{
    /// <summary>
    /// Encja domenowa dla tabeli [auth].[RefreshToken].
    /// </summary>
    public class RefreshToken
    {
        public long IdRefreshToken { get; set; }
        public int IdUzytkownika { get; set; }
        public byte[] TokenHash { get; set; } = Array.Empty<byte>();
        public DateTime ExpiresAtUtc { get; set; }
        public DateTime? RevokedAtUtc { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public string? DeviceInfo { get; set; }
        public string? IpAddress { get; set; }

        public virtual Uzytkownik Uzytkownik { get; set; } = null!;
    }
}