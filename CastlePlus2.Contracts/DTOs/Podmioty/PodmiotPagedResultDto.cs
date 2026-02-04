namespace CastlePlus2.Contracts.DTOs.Podmioty
{
    public class PodmiotPagedResultDto
    {
        public List<PodmiotDto> Items { get; set; } = new();
        public int TotalCount { get; set; }
    }
}