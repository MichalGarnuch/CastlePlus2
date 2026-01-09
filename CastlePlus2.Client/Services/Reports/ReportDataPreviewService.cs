using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Contracts.Reports;

namespace CastlePlus2.Client.Services.Reports;

public sealed class ReportDataPreviewService : IReportDataPreviewService
{
    private readonly HttpClient _httpClient;

    public ReportDataPreviewService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ReportDataPreviewResponse> GetPreviewAsync(string reportKey, int take, CancellationToken ct = default)
    {
        var url = $"/api/raporty/podglad-danych?reportKey={Uri.EscapeDataString(reportKey)}&take={take}";
        var response = await _httpClient.GetFromJsonAsync<ReportDataPreviewResponse>(url, ct);

        if (response is null)
        {
            throw new InvalidOperationException("Brak odpowiedzi z podglądu danych.");
        }

        return response;
    }
}