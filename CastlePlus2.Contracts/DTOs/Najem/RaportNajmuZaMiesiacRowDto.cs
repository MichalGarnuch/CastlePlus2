namespace CastlePlus2.Contracts.DTOs.Najem
{
    public class RaportNajmuZaMiesiacRowDto
    {
        public Guid IdUmowyNajmu { get; set; }
        public DateTime DataPoczatku { get; set; }
        public DateTime? DataZakonczenia { get; set; }
        public string KodWaluty { get; set; } = string.Empty;
        public string? Wynajmujacy { get; set; }
        public string? Najemca { get; set; }
        public decimal KwotaMiesiecznaBazowa { get; set; }
        public long LiczbaSkladnikow { get; set; }
        public int LiczbaPrzedmiotow { get; set; }
        public string? Przedmioty { get; set; }
    }
}