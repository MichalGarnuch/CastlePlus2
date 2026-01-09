using System;
using System.Collections.Generic;

namespace CastlePlus2.Contracts.Reports;

public sealed record ReportDataPreviewResponse(
    string ReportKey,
    string Title,
    DateTime GeneratedAtUtc,
    IReadOnlyList<string> Columns,
    IReadOnlyList<IReadOnlyList<string>> Rows,
    IReadOnlyDictionary<string, string>? Summary);