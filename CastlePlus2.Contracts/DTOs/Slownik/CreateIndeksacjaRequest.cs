namespace CastlePlus2.Contracts.DTOs.Slowniki
{
    public class CreateIndeksacjaRequest
    {
        public string KodIndeksacji { get; set; } = default!;
        public string Nazwa { get; set; } = default!;
        public string? AdresZrodlaURL { get; set; }
    }
}
