using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Reports;
using CastlePlus2.Contracts.Reports;

namespace CastlePlus2.Infrastructure.Services.Reports.Definitions;

public sealed class FakturyReportDefinition : IReportDefinition
{
    private const int DefaultTake = 50;
    private readonly IReportsReadService _reportsReadService;

    public FakturyReportDefinition(IReportsReadService reportsReadService)
    {
        _reportsReadService = reportsReadService;
    }

    public string Key => "faktury";
    public string Title => "Statystyki faktur";
    public string FileNameBase => "statystyki_faktur";
    public Type RowType => typeof(FakturyStatRow);

    public Task<IReadOnlyList<object>> BuildRowsAsync(CancellationToken ct)
    {
        return BuildRowsAsync(DefaultTake, ct);
    }

    public async Task<IReadOnlyList<object>> BuildRowsAsync(int take, CancellationToken ct)
    {
        var rows = await _reportsReadService.GetFakturyAsync(take, ct);
        return rows.Cast<object>().ToList();
    }
}