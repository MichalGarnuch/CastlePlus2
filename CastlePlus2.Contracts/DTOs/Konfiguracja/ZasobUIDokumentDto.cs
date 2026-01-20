using System;

namespace CastlePlus2.Contracts.DTOs.Konfiguracja;

public class ZasobUIDokumentDto
{
    public long IdDokumentu { get; set; }
    public string Nazwa { get; set; } = string.Empty;
    public string Opis { get; set; } = string.Empty;
    public string SciezkaPliku { get; set; } = string.Empty;
}
