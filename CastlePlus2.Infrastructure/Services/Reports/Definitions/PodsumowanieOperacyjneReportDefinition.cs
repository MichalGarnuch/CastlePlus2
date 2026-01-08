using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Reports;
using CastlePlus2.Contracts.Reports;

namespace CastlePlus2.Infrastructure.Services.Reports.Definitions;

public sealed class PodsumowanieOperacyjneReportDefinition : IReportDefinition
{
    private readonly IReportsReadService _reportsReadService;

    public PodsumowanieOperacyjneReportDefinition(IReportsReadService reportsReadService)
    {
        _reportsReadService = reportsReadService;
    }

    public string Key => "podsumowanie";
    public string Title => "Podsumowanie operacyjne";
    public string FileNameBase => "podsumowanie_operacyjne";
    public Type RowType => typeof(PodsumowanieOperacyjneRow);

    public async Task<IReadOnlyList<object>> BuildRowsAsync(CancellationToken ct)
    {
        var row = await _reportsReadService.GetPodsumowanieAsync(ct);
        return new List<object> { row };
    }
}