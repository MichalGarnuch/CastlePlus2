using System;

namespace CastlePlus2.Contracts.DTOs.Dokumenty
{
    public class DokumentDto
    {
        public long IdDokumentu { get; set; }
        public Guid? IdEncjiOwner { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public string? Opis { get; set; }
        public string SciezkaPliku { get; set; } = string.Empty;
        public DateTime DataUtworzenia { get; set; }
    }
}
