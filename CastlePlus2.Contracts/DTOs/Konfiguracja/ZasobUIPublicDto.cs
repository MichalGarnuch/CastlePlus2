using System;
using System.Collections.Generic;

namespace CastlePlus2.Contracts.DTOs.Konfiguracja;

public class ZasobUIPublicDto
{
    public Guid IdEncji { get; set; }
    public string KodZasobu { get; set; } = string.Empty;
    public string Typ { get; set; } = string.Empty;
    public string? Kategoria { get; set; }
    public int Sort { get; set; }

    public List<ZasobUITekstDto> Teksty { get; set; } = new();
    public List<ZasobUIDokumentDto> Dokumenty { get; set; } = new();
}
