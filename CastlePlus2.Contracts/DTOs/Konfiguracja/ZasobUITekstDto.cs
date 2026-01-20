using System;

namespace CastlePlus2.Contracts.DTOs.Konfiguracja;

public class ZasobUITekstDto
{
    public long IdZasobuTekstu { get; set; }
    public Guid IdEncji { get; set; }
    public string Jezyk { get; set; } = string.Empty;
    public string Pole { get; set; } = string.Empty;
    public string Wartosc { get; set; } = string.Empty;
    public string Format { get; set; } = string.Empty;
    public int Sort { get; set; }
}
