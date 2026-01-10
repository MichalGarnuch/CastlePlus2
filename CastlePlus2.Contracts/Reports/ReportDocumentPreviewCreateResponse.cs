using System;

namespace CastlePlus2.Contracts.Reports;

public sealed record ReportDocumentPreviewCreateResponse(
    string PreviewId,
    string StreamUrl,
    DateTime ExpiresAtUtc,
    string ContentType,
    string FileName);