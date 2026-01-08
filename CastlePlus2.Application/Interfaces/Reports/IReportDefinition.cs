using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CastlePlus2.Application.Interfaces.Reports;

public interface IReportDefinition
{
    string Key { get; }
    string Title { get; }
    string FileNameBase { get; }
    Type RowType { get; }
    Task<IReadOnlyList<object>> BuildRowsAsync(CancellationToken ct);
}