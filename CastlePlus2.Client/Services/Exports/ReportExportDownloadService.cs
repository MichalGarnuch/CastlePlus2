using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CastlePlus2.Contracts.Exports;

namespace CastlePlus2.Client.Services.Exports;

public sealed class ReportExportDownloadService : IReportExportDownloadService
{
    private readonly HttpClient _http;

    public ReportExportDownloadService(HttpClient http) => _http = http;

    public async Task<ReportExportLinkResponse> CreateLinkAsync(string reportKey, ExportFormat format, bool archive, int take)
    {
        var url =
            $"api/raporty/eksport-link" +
            $"?reportKey={reportKey}" +
            $"&format={format}" +
            $"&archive={(archive ? "true" : "false")}" +
            $"&take={take}";

        return await _http.GetFromJsonAsync<ReportExportLinkResponse>(url)
               ?? throw new InvalidOperationException("Brak odpowiedzi API");
    }

    public string ToAbsolute(string relative)
        => new Uri(_http.BaseAddress!, relative).ToString();
}
