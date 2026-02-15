using CastlePlus2.Application.Interfaces.Dashboard;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Services.Dashboard
{
    public class NajemPowerDashboardDataService : INajemPowerDashboardDataService
    {
        private readonly CastlePlus2DbContext _db;

        public NajemPowerDashboardDataService(CastlePlus2DbContext db)
        {
            _db = db;
        }

        public async Task<DateOnly> GetMinAvailableDateAsync(CancellationToken ct)
        {
            var minContractDateTime = await _db.UmowyNajmu
                .AsNoTracking()
                .MinAsync(x => (DateTime?)x.DataPoczatku, ct);

            var minInvoiceDateTime = await _db.Faktury
                .AsNoTracking()
                .MinAsync(x => (DateTime?)x.DataWystawienia, ct);

            var minDateTime = minContractDateTime ?? minInvoiceDateTime ?? DateTime.Today;
            return DateOnly.FromDateTime(minDateTime.Date);
        }

        public async Task<Dictionary<Guid, long>> GetContractTenantMapAsync(IReadOnlyCollection<Guid> contractIds, CancellationToken ct)
        {
            if (contractIds.Count == 0)
            {
                return new Dictionary<Guid, long>();
            }

            return await _db.UmowyNajmu
                .AsNoTracking()
                .Where(x => contractIds.Contains(x.Id))
                .ToDictionaryAsync(x => x.Id, x => x.IdNajemcy, ct);
        }

        public async Task<Dictionary<long, long>> GetInvoicePodmiotMapAsync(IReadOnlyCollection<long> invoiceIds, CancellationToken ct)
        {
            if (invoiceIds.Count == 0)
            {
                return new Dictionary<long, long>();
            }

            return await _db.Faktury
                .AsNoTracking()
                .Where(x => invoiceIds.Contains(x.IdFaktury))
                .ToDictionaryAsync(x => x.IdFaktury, x => x.IdPodmiotu, ct);
        }

        public async Task<int> GetActiveContractsCountAsync(DateOnly today, Guid? buildingId, CancellationToken ct)
        {
            var activePrzedmioty = _db.PrzedmiotyNajmu
                .AsNoTracking()
                .Where(x => x.OdDnia <= today && (x.DoDnia == null || x.DoDnia >= today));

            if (buildingId.HasValue)
            {
                var lokalIds = await _db.Lokale
                    .AsNoTracking()
                    .Where(x => x.IdBudynku == buildingId.Value)
                    .Select(x => x.Id)
                    .ToListAsync(ct);

                activePrzedmioty = activePrzedmioty.Where(x => lokalIds.Contains(x.IdEncji));
            }

            return await activePrzedmioty
                .Select(x => x.IdUmowyNajmu)
                .Distinct()
                .CountAsync(ct);
        }

        public async Task<List<NajemPowerOccupancyDataRow>> GetOccupancyRowsAsync(DateOnly today, Guid? buildingId, CancellationToken ct)
        {
            var lokaleQuery = _db.Lokale.AsNoTracking();
            if (buildingId.HasValue)
            {
                lokaleQuery = lokaleQuery.Where(x => x.IdBudynku == buildingId.Value);
            }

            var lokale = await lokaleQuery
                .Select(x => new { x.Id, x.KodLokalu, x.IdBudynku })
                .ToListAsync(ct);

            var localIds = lokale.Select(x => x.Id).ToList();
            var activeAssignments = await _db.PrzedmiotyNajmu
                .AsNoTracking()
                .Where(x => localIds.Contains(x.IdEncji)
                            && x.OdDnia <= today
                            && (x.DoDnia == null || x.DoDnia >= today))
                .GroupBy(x => x.IdEncji)
                .Select(g => new { LokalId = g.Key, ContractId = g.Select(x => x.IdUmowyNajmu).FirstOrDefault() })
                .ToListAsync(ct);

            var contractIds = activeAssignments
                .Select(x => x.ContractId)
                .Where(x => x != Guid.Empty)
                .Distinct()
                .ToList();

            var contractCodes = contractIds.Count == 0
                ? new Dictionary<Guid, string?>()
                : await _db.UmowyNajmu
                    .AsNoTracking()
                    .Where(x => contractIds.Contains(x.Id))
                    .ToDictionaryAsync(x => x.Id, x => x.KodEncji, ct);

            return lokale.Select(l =>
            {
                var assignment = activeAssignments.FirstOrDefault(x => x.LokalId == l.Id);
                var isRented = assignment is not null && assignment.ContractId != Guid.Empty;

                return new NajemPowerOccupancyDataRow
                {
                    LokalId = l.Id,
                    LokalCode = l.KodLokalu,
                    BudynekId = l.IdBudynku,
                    ContractId = isRented ? assignment!.ContractId : null,
                    ContractCode = isRented && contractCodes.TryGetValue(assignment!.ContractId, out var code) ? code : null
                };
            }).ToList();
        }
    }
}