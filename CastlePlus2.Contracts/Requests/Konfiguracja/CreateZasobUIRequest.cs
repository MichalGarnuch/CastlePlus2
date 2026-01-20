using System;

namespace CastlePlus2.Contracts.Requests.Konfiguracja;

public class CreateZasobUIRequest
{
    public string KodZasobu { get; set; } = string.Empty;
    public string Typ { get; set; } = string.Empty;
    public string? Kategoria { get; set; }
    public bool CzyAktywny { get; set; }
    public int Sort { get; set; }
    public DateTime? WazneOdUtc { get; set; }
    public DateTime? WazneDoUtc { get; set; }
}
