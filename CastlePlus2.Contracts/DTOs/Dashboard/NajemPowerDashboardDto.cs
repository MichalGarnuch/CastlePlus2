namespace CastlePlus2.Contracts.DTOs.Dashboard
{
    public class NajemPowerDashboardDto
    {
        public NajemPowerAppliedRangeDto AppliedRange { get; set; } = new();
        public NajemPowerActiveFiltersDto ActiveFilters { get; set; } = new();
        public NajemPowerDrillDownStateDto DrillDownState { get; set; } = new();

        public NajemPowerKpiDto Kpi { get; set; } = new();
        public List<NajemPowerSeriesItemDto> OccupancySeries { get; set; } = new();
        public List<NajemPowerSeriesItemDto> OverdueAgingSeries { get; set; } = new();
        public string OverdueAgeBasisDescription { get; set; } = string.Empty;

        public List<WygasajacaUmowaDto> EndingContracts { get; set; } = new();
        public List<DashboardV1ZaleglaFakturaDto> TopOverdueInvoices { get; set; } = new();
        public List<NajemPowerOccupancyItemDto> OccupancyItems { get; set; } = new();

        public bool HasDataOverall { get; set; }
        public bool HasOverdueData { get; set; }
        public bool HasEndingContractsData { get; set; }

        public string? OverallMessage { get; set; }
        public string? OverdueMessage { get; set; }
        public string? EndingContractsMessage { get; set; }
    }

    public class NajemPowerAppliedRangeDto
    {
        public DateOnly DateFrom { get; set; }
        public DateOnly DateTo { get; set; }
    }

    public class NajemPowerActiveFiltersDto
    {
        public bool OnlyOverdue { get; set; }
        public bool UseEndingInDays { get; set; }
        public int EndingInDays { get; set; }
        public Guid? IdBudynek { get; set; }
        public string? RangeLabel { get; set; }
        public List<string> ActiveSelections { get; set; } = new();
    }

    public class NajemPowerDrillDownStateDto
    {
        public string? SelectedBucket { get; set; }
        public long? SelectedNajemcaId { get; set; }
        public Guid? SelectedUmowaId { get; set; }
        public string? SelectedOccupancySegment { get; set; }
    }

    public class NajemPowerKpiDto
    {
        public decimal OccupancyPercent { get; set; }
        public int ActiveContractsCount { get; set; }
        public decimal OverdueAmount { get; set; }
        public int EndingContractsCount { get; set; }
    }

    public class NajemPowerSeriesItemDto
    {
        public string Key { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public decimal Value { get; set; }
    }

    public class NajemPowerOccupancyItemDto
    {
        public Guid LokalId { get; set; }
        public string LokalCode { get; set; } = string.Empty;
        public Guid BudynekId { get; set; }
        public string OccupancySegment { get; set; } = string.Empty;
        public Guid? ContractId { get; set; }
        public string? ContractCode { get; set; }
    }
}