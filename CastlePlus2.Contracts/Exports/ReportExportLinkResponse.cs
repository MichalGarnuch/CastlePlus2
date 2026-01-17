namespace CastlePlus2.Contracts.Exports;

public sealed record ReportExportLinkResponse(
    string DownloadUrl,
    DateTime ExpiresAtUtc
);
