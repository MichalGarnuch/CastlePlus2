namespace CastlePlus2.Contracts.DTOs.Finanse
{
    public class GenerateFakturaWydrukResponse
    {
        public string DownloadUrl { get; set; } = string.Empty;
        public DateTime ExpiresAtUtc { get; set; }
        public List<string> Warnings { get; set; } = new();
    }
}