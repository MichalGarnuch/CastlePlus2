namespace CastlePlus2.Contracts.Requests.Slownik
{
    public class UpdateIndeksacjaRequest
    {
        public string Nazwa { get; set; } = default!;
        public string? AdresZrodlaURL { get; set; }
    }
}
