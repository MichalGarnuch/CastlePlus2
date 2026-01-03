namespace CastlePlus2.Contracts.DTOs.Dashboard
{
    public class WygasajacaUmowaDto
    {
        // Technicznie nadal trzymamy Id (np. do nawigacji w przyszłości),
        // ale UI nie musi go wyświetlać.
        public Guid IdUmowy { get; set; }
        public string? KodUmowy { get; set; }

        public DateOnly? DataZakonczenia { get; set; } // dla bezterminowych będzie null

        public string Najemca { get; set; } = string.Empty;
        public string Wynajmujacy { get; set; } = string.Empty;
        public string PrzedmiotNajmu { get; set; } = string.Empty;
    }
}
