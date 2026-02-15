using System.Net;
using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Dokumenty;
using CastlePlus2.Contracts.Requests.Dokumenty;
using Microsoft.Extensions.Logging;

namespace CastlePlus2.Client.Services.Dokumenty;

public sealed class DokumentyService : IDokumentyService
{
    private readonly HttpClient _http;
    private readonly ILogger<DokumentyService> _logger;

    private const string BaseUrl = "api/dokumenty";

    public DokumentyService(HttpClient http, ILogger<DokumentyService> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<List<DokumentDto>> GetAllAsync(string? search = null, CancellationToken ct = default)
    {
        var url = string.IsNullOrWhiteSpace(search)
            ? BaseUrl
            : $"{BaseUrl}?search={Uri.EscapeDataString(search.Trim())}";

        return await _http.GetFromJsonAsync<List<DokumentDto>>(url, ct) ?? new();
    }

    public async Task<DokumentDto?> GetByIdAsync(long idDokumentu, CancellationToken ct = default)
    {
        return await _http.GetFromJsonAsync<DokumentDto>($"{BaseUrl}/{idDokumentu}", ct);
    }

    public async Task<DokumentDto> CreateAsync(CreateDokumentRequest request, CancellationToken ct = default)
    {
        var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
        resp.EnsureSuccessStatusCode();
        return (await resp.Content.ReadFromJsonAsync<DokumentDto>(cancellationToken: ct))!;
    }

    // ZMIANA: zwraca bool, false dla 404
    public async Task<bool> UpdateAsync(long idDokumentu, UpdateDokumentRequest request, CancellationToken ct = default)
    {
        var resp = await _http.PutAsJsonAsync($"{BaseUrl}/{idDokumentu}", request, ct);

        if (resp.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogInformation("Dokument (Id={Id}) nie istnieje (404) - Update.", idDokumentu);
            return false;
        }

        resp.EnsureSuccessStatusCode();
        return true;
    }

    // ZMIANA: zwraca bool, false dla 404
    public async Task<bool> DeleteAsync(long idDokumentu, CancellationToken ct = default)
    {
        var resp = await _http.DeleteAsync($"{BaseUrl}/{idDokumentu}", ct);

        if (resp.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogInformation("Dokument (Id={Id}) nie istnieje (404) - Delete.", idDokumentu);
            return false;
        }

        resp.EnsureSuccessStatusCode();
        return true;
    }

    public async Task<List<DokumentDto>> GetByEncjaIdAsync(Guid idEncji, CancellationToken ct = default)
    {
        return await _http.GetFromJsonAsync<List<DokumentDto>>($"{BaseUrl}/by-encja/{idEncji}", ct) ?? new();
    }

    public async Task<byte[]> DownloadAsync(long idDokumentu, CancellationToken ct = default)
    {
        return await _http.GetByteArrayAsync($"{BaseUrl}/{idDokumentu}/download", ct);
    }
}
