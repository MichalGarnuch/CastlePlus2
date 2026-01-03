namespace CastlePlus2.Contracts.DTOs.Dashboard
{
    public class WygasajacaUmowaDto
    {
        // Technicznie nadal trzymamy Id (np. do nawigacji w przyszłości),
        // ale UI nie musi go wyświetlać.
        public Guid IdUmowy { get; set; }

        // Preferowany “ludzki” identyfikator umowy (np. UN/2026/0001)
        public string? KodUmowy { get; set; }

        public DateOnly DataZakonczenia { get; set; }

        // Nazwy stron (zamiast Id)
        public string Najemca { get; set; } = string.Empty;
        public string Wynajmujacy { get; set; } = string.Empty;

        // “Lokalizacja / przedmiot” – np. "BUD-001: LOK-12, LOK-13"
        public string PrzedmiotNajmu { get; set; } = string.Empty;
    }
}
