using System;
using System.Collections.Generic;

namespace CastlePlus2.Contracts.DTOs.Konfiguracja;

public class ZasobUIDto
{
    public Guid IdEncji { get; set; }
    public string KodZasobu { get; set; } = string.Empty;
    public string Typ { get; set; } = string.Empty;
    public string? Kategoria { get; set; }
    public bool CzyAktywny { get; set; }
    public int Sort { get; set; }

    public DateTime? WazneOdUtc { get; set; }
    public DateTime? WazneDoUtc { get; set; }
    public DateTime UtworzonoUtc { get; set; }
    public DateTime? ZmienionoUtc { get; set; }

    public List<ZasobUITekstDto> Teksty { get; set; } = new();
    public List<ZasobUIDokumentDto> Dokumenty { get; set; } = new();
}
