namespace CastlePlus2.Contracts.Requests.Dashboard
{
    public class GetNajemPowerDashboardRequest
    {
        public DateOnly? DateFrom { get; set; }
        public DateOnly? DateTo { get; set; }
        public bool OnlyOverdue { get; set; }
        public bool UseEndingInDays { get; set; } = true;
        public int? EndingInDays { get; set; } = 30;
        public Guid? IdBudynek { get; set; }
        public NajemPowerDrillDownRequest? DrillDown { get; set; }
    }

    public class NajemPowerDrillDownRequest
    {
        public string? SelectedBucket { get; set; }
        public long? SelectedNajemcaId { get; set; }
        public Guid? SelectedUmowaId { get; set; }
        public string? SelectedOccupancySegment { get; set; }
    }
}