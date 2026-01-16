namespace CastlePlus2.Contracts.DTOs.Auth
{
    public sealed class RoleDto
    {
        public int IdRoli { get; set; }
        public string Kod { get; set; } = string.Empty;
        public string Nazwa { get; set; } = string.Empty;
    }
}