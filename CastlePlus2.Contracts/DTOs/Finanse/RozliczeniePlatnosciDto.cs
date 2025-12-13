namespace CastlePlus2.Contracts.DTOs.Finanse
{
    public class RozliczeniePlatnosciDto
    {
        public long IdRozliczenia { get; set; }
        public long IdPlatnosci { get; set; }
        public long IdFaktury { get; set; }
        public decimal Kwota { get; set; }
    }
}
