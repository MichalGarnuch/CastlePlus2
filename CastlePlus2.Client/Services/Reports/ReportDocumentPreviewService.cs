using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Contracts.Reports;

namespace CastlePlus2.Client.Services.Reports;

public sealed class ReportDocumentPreviewService : IReportDocumentPreviewService
{
    private readonly HttpClient _httpClient;

    public ReportDocumentPreviewService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ReportDocumentPreviewCreateResponse> CreatePdfPreviewAsync(
        string reportKey,
        int take,
        CancellationToken ct = default)
    {
        var url = $"/api/raporty/podglad-dokumentu?reportKey={Uri.EscapeDataString(reportKey)}&take={take}";
        var response = await _httpClient.GetFromJsonAsync<ReportDocumentPreviewCreateResponse>(url, ct);

        if (response is null)
        {
            throw new InvalidOperationException("Brak odpowiedzi z podglądu dokumentu.");
        }

        return response;
    }
}
