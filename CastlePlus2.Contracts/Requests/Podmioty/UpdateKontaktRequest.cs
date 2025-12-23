namespace CastlePlus2.Contracts.Requests.Podmioty
{
    public class UpdateKontaktRequest
    {
        public long IdPodmiotu { get; set; }
        public string Rodzaj { get; set; } = string.Empty;
        public string Wartosc { get; set; } = string.Empty;
    }
}