namespace CastlePlus2.Application.Interfaces.Dashboard
{
    public interface INajemPowerDashboardDataService
    {
        Task<DateOnly> GetMinAvailableDateAsync(CancellationToken ct);
        Task<Dictionary<Guid, long>> GetContractTenantMapAsync(IReadOnlyCollection<Guid> contractIds, CancellationToken ct);
        Task<Dictionary<long, long>> GetInvoicePodmiotMapAsync(IReadOnlyCollection<long> invoiceIds, CancellationToken ct);
        Task<int> GetActiveContractsCountAsync(DateOnly today, Guid? buildingId, CancellationToken ct);
        Task<List<NajemPowerOccupancyDataRow>> GetOccupancyRowsAsync(DateOnly today, Guid? buildingId, CancellationToken ct);
    }

    public class NajemPowerOccupancyDataRow
    {
        public Guid LokalId { get; set; }
        public string LokalCode { get; set; } = string.Empty;
        public Guid BudynekId { get; set; }
        public Guid? ContractId { get; set; }
        public string? ContractCode { get; set; }
    }
}