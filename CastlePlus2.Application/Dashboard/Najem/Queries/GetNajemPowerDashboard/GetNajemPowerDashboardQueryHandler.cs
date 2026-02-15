using CastlePlus2.Application.Interfaces.Dashboard;
using CastlePlus2.Contracts.DTOs.Dashboard;
using CastlePlus2.Contracts.Requests.Dashboard;
using MediatR;

namespace CastlePlus2.Application.Dashboard.Najem.Queries.GetNajemPowerDashboard
{
    public sealed class GetNajemPowerDashboardQueryHandler : IRequestHandler<GetNajemPowerDashboardQuery, NajemPowerDashboardDto>
    {
        private readonly INajemDashboardQueryService _najemDashboardQueryService;
        private readonly IDashboardV1NajemQueryService _dashboardV1NajemQueryService;
        private readonly INajemPowerDashboardDataService _dataService;

        public GetNajemPowerDashboardQueryHandler(
            INajemDashboardQueryService najemDashboardQueryService,
            IDashboardV1NajemQueryService dashboardV1NajemQueryService,
            INajemPowerDashboardDataService dataService)
        {
            _najemDashboardQueryService = najemDashboardQueryService;
            _dashboardV1NajemQueryService = dashboardV1NajemQueryService;
            _dataService = dataService;
        }

        public async Task<NajemPowerDashboardDto> Handle(GetNajemPowerDashboardQuery query, CancellationToken ct)
        {
            var request = query.Request ?? new GetNajemPowerDashboardRequest();
            var today = DateOnly.FromDateTime(DateTime.Today);

            var minDate = await _dataService.GetMinAvailableDateAsync(ct);
            var dateFrom = request.DateFrom ?? minDate;
            var dateTo = request.DateTo ?? today;

            if (request.DateFrom.HasValue && !request.DateTo.HasValue)
                dateTo = today;
            else if (!request.DateFrom.HasValue && request.DateTo.HasValue)
                dateFrom = minDate;

            if (dateFrom > dateTo)
                (dateFrom, dateTo) = (dateTo, dateFrom);

            var endingDays = request.EndingInDays.GetValueOrDefault(30);
            if (endingDays < 1) endingDays = 30;

            // Ending contracts window:
            // - gdy UseEndingInDays=true: od dateTo do dateTo + X dni
            // - inaczej: w ramach globalnego zakresu dateFrom..dateTo
            var endingFrom = request.UseEndingInDays ? dateTo : dateFrom;
            var endingTo = request.UseEndingInDays ? dateTo.AddDays(endingDays) : dateTo;

            var najemBase = await _najemDashboardQueryService.GetNajemDashboardAsync(endingFrom, endingTo, ct);
            var v1Base = await _dashboardV1NajemQueryService.GetDashboardV1NajemAsync(dateFrom, dateTo, ct);

            // ====== ENDING CONTRACTS (drilldown: umowa/najemca) ======
            var endingContracts = najemBase.WygasajaceUmowy
                .Where(x => x.DataZakonczenia.HasValue
                            && x.DataZakonczenia.Value >= endingFrom
                            && x.DataZakonczenia.Value <= endingTo)
                .OrderBy(x => x.DataZakonczenia)
                .ToList();

            var selectedNajemcaId = request.DrillDown?.SelectedNajemcaId;
            var selectedUmowaId = request.DrillDown?.SelectedUmowaId;

            // Jeżeli user wybrał Umowę, a nie wybrał Najemcy, to dociągnij Najemcę tylko dla tej jednej umowy (bez masowego IN)
            if (!selectedNajemcaId.HasValue && selectedUmowaId.HasValue)
            {
                var single = await _dataService.GetContractTenantMapAsync(new[] { selectedUmowaId.Value }, ct);
                if (single.TryGetValue(selectedUmowaId.Value, out var tenantId))
                    selectedNajemcaId = tenantId;
            }

            if (selectedUmowaId.HasValue)
            {
                endingContracts = endingContracts.Where(x => x.IdUmowy == selectedUmowaId.Value).ToList();
            }
            else if (selectedNajemcaId.HasValue && endingContracts.Count > 0)
            {
                var contractIds = endingContracts.Select(x => x.IdUmowy).Distinct().ToList();
                var contractTenantPairs = await _dataService.GetContractTenantMapAsync(contractIds, ct);

                endingContracts = endingContracts
                    .Where(x => contractTenantPairs.TryGetValue(x.IdUmowy, out var tenantId) && tenantId == selectedNajemcaId.Value)
                    .ToList();
            }

            // ====== OVERDUE INVOICES (serie/suma z pełnego zbioru, tabela = TOP10) ======
            var overdueInvoiceBase = v1Base.ZalegleFaktury;

            var overdueInvoicesAll = overdueInvoiceBase
                .Where(x => !request.OnlyOverdue || x.KwotaPozostala > 0m)
                .ToList();

            if (selectedNajemcaId.HasValue && overdueInvoicesAll.Count > 0)
            {
                var overdueIds = overdueInvoicesAll.Select(x => x.IdFaktury).Distinct().ToList();
                var invoicePodmiotMap = await _dataService.GetInvoicePodmiotMapAsync(overdueIds, ct);

                overdueInvoicesAll = overdueInvoicesAll
                    .Where(x => invoicePodmiotMap.TryGetValue(x.IdFaktury, out var podmiotId) && podmiotId == selectedNajemcaId.Value)
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(request.DrillDown?.SelectedBucket))
            {
                overdueInvoicesAll = overdueInvoicesAll
                    .Where(x => GetBucketKey(today.DayNumber - x.DataWystawienia.DayNumber) == request.DrillDown!.SelectedBucket)
                    .ToList();
            }

            var overdueAgingSeries = overdueInvoicesAll
                .GroupBy(x => GetBucketKey(today.DayNumber - x.DataWystawienia.DayNumber))
                .Select(g => new NajemPowerSeriesItemDto
                {
                    Key = g.Key,
                    Label = BucketLabel(g.Key),
                    Value = g.Sum(x => x.KwotaPozostala)
                })
                .OrderBy(x => x.Key)
                .ToList();

            var overdueAmount = overdueInvoicesAll.Sum(x => x.KwotaPozostala);

            var topOverdueInvoices = overdueInvoicesAll
                .OrderByDescending(x => x.KwotaPozostala)
                .Take(10)
                .ToList();

            // ====== OCCUPANCY (KPI/serie z bazowego zbioru, drilldown segmentu tylko do tabeli szczegółów) ======
            var activeContractsCount = await _dataService.GetActiveContractsCountAsync(today, request.IdBudynek, ct);

            var occupancyRows = await _dataService.GetOccupancyRowsAsync(today, request.IdBudynek, ct);

            var occupancyItemsBase = occupancyRows.Select(x => new NajemPowerOccupancyItemDto
            {
                LokalId = x.LokalId,
                LokalCode = x.LokalCode,
                BudynekId = x.BudynekId,
                OccupancySegment = x.ContractId.HasValue ? "rented" : "vacant",
                ContractId = x.ContractId,
                ContractCode = x.ContractCode
            }).ToList();

            var occupancySeries = new List<NajemPowerSeriesItemDto>
            {
                new() { Key = "rented", Label = "Wynajęte", Value = occupancyItemsBase.Count(x => x.OccupancySegment == "rented") },
                new() { Key = "vacant", Label = "Wolne", Value = occupancyItemsBase.Count(x => x.OccupancySegment == "vacant") }
            };

            var occupancyTotal = occupancySeries.Sum(x => x.Value);
            var rentedCount = occupancySeries.First(x => x.Key == "rented").Value;
            var occupancyPercent = occupancyTotal == 0 ? 0m : decimal.Round((rentedCount / occupancyTotal) * 100m, 2);

            // Drill-down segmentu: tylko tabela szczegółowa
            var occupancyItems = occupancyItemsBase;
            if (!string.IsNullOrWhiteSpace(request.DrillDown?.SelectedOccupancySegment))
            {
                occupancyItems = occupancyItems
                    .Where(x => string.Equals(x.OccupancySegment, request.DrillDown.SelectedOccupancySegment, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // ====== FLAGS ======
            var hasOverdueData = overdueInvoicesAll.Count > 0;
            var hasEndingData = endingContracts.Count > 0;
            var hasOccupancyDataBase = occupancyItemsBase.Count > 0;

            var hasOverallData = hasOverdueData || hasEndingData || hasOccupancyDataBase;

            return new NajemPowerDashboardDto
            {
                AppliedRange = new NajemPowerAppliedRangeDto
                {
                    DateFrom = dateFrom,
                    DateTo = dateTo
                },
                ActiveFilters = new NajemPowerActiveFiltersDto
                {
                    OnlyOverdue = request.OnlyOverdue,
                    UseEndingInDays = request.UseEndingInDays,
                    EndingInDays = endingDays,
                    IdBudynek = request.IdBudynek,
                    RangeLabel = $"Dane za okres: {dateFrom:yyyy-MM-dd} – {dateTo:yyyy-MM-dd}",
                    ActiveSelections = BuildActiveSelections(request, selectedNajemcaId, selectedUmowaId)
                },
                DrillDownState = new NajemPowerDrillDownStateDto
                {
                    SelectedBucket = request.DrillDown?.SelectedBucket,
                    SelectedNajemcaId = selectedNajemcaId,
                    SelectedUmowaId = selectedUmowaId,
                    SelectedOccupancySegment = request.DrillDown?.SelectedOccupancySegment
                },
                Kpi = new NajemPowerKpiDto
                {
                    OccupancyPercent = occupancyPercent,
                    ActiveContractsCount = activeContractsCount,
                    OverdueAmount = overdueAmount,
                    EndingContractsCount = endingContracts.Count
                },
                OccupancySeries = occupancySeries,
                OverdueAgingSeries = overdueAgingSeries,
                OverdueAgeBasisDescription = "Wiek zaległości liczony od daty wystawienia faktury (brak terminu płatności w modelu).",
                EndingContracts = endingContracts,
                TopOverdueInvoices = topOverdueInvoices,
                OccupancyItems = occupancyItems.OrderBy(x => x.LokalCode).ToList(),
                HasDataOverall = hasOverallData,
                HasOverdueData = hasOverdueData,
                HasEndingContractsData = hasEndingData,
                OverallMessage = hasOverallData ? null : "Brak danych dla wybranych filtrów.",
                OverdueMessage = hasOverdueData ? null : "Brak zaległości w wybranym okresie.",
                EndingContractsMessage = hasEndingData ? null : "Brak umów kończących się w wybranym okresie."
            };
        }

        private static string GetBucketKey(int ageDays)
        {
            if (ageDays <= 30) return "0-30";
            if (ageDays <= 60) return "31-60";
            return "61+";
        }

        private static string BucketLabel(string key) => key switch
        {
            "0-30" => "0–30 dni",
            "31-60" => "31–60 dni",
            _ => "61+ dni"
        };

        private static string SegmentLabel(string? key) => key?.ToLowerInvariant() switch
        {
            "rented" => "Wynajęte",
            "vacant" => "Wolne",
            _ => key ?? string.Empty
        };

        private static List<string> BuildActiveSelections(GetNajemPowerDashboardRequest request, long? selectedNajemcaId, Guid? selectedUmowaId)
        {
            var selections = new List<string>();

            if (!string.IsNullOrWhiteSpace(request.DrillDown?.SelectedBucket))
            {
                selections.Add($"Kubełek zaległości: {BucketLabel(request.DrillDown.SelectedBucket)}");
            }

            if (selectedNajemcaId.HasValue)
            {
                selections.Add($"Najemca ID: {selectedNajemcaId}");
            }

            if (selectedUmowaId.HasValue)
            {
                selections.Add($"Umowa ID: {selectedUmowaId}");
            }

            if (!string.IsNullOrWhiteSpace(request.DrillDown?.SelectedOccupancySegment))
            {
                selections.Add($"Obłożenie: {SegmentLabel(request.DrillDown.SelectedOccupancySegment)}");
            }

            return selections;
        }
    }
}
