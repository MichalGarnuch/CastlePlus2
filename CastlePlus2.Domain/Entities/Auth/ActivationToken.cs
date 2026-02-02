namespace CastlePlus2.Domain.Entities.Auth
{
    public sealed class ActivationToken
    {
        public int IdActivationToken { get; set; }
        public int IdUzytkownika { get; set; }
        public byte[] TokenHash { get; set; } = Array.Empty<byte>();
        public DateTime CreatedAtUtc { get; set; }
        public DateTime ExpiresAtUtc { get; set; }
        public DateTime? UsedAtUtc { get; set; }
    }
}