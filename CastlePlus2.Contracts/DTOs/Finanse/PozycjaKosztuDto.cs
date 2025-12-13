namespace CastlePlus2.Contracts.DTOs.Finanse
{
    public class PozycjaKosztuDto
    {
        public long IdPozycjiKosztu { get; set; }
        public long IdFaktury { get; set; }
        public long IdKategoriiKosztu { get; set; }
        public string? Opis { get; set; }
        public decimal KwotaNetto { get; set; }
        public decimal KwotaBrutto { get; set; }
    }
}
