using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Common;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;

namespace CastlePlus2.Client.Services.Najem
{
    public class PrzedmiotyNajmuService : IPrzedmiotyNajmuService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "api/najem/przedmiotynajmu";

        public PrzedmiotyNajmuService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PrzedmiotNajmuDto>> GetAllAsync(CancellationToken ct = default)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, BaseUrl);
            req.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            req.Headers.Pragma.ParseAdd("no-cache");

            var resp = await _http.SendAsync(req, ct);
            resp.EnsureSuccessStatusCode();

            return await resp.Content.ReadFromJsonAsync<List<PrzedmiotNajmuDto>>(cancellationToken: ct) ?? new();
        }

        public async Task<PrzedmiotNajmuDto?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/{id}");
            req.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            req.Headers.Pragma.ParseAdd("no-cache");

            var resp = await _http.SendAsync(req, ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return null;

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<PrzedmiotNajmuDto>(cancellationToken: ct);
        }

        public async Task<PagedResultDto<PrzedmiotNajmuLookupDto>> SearchLookupPagedAsync(
            string? q,
            Guid? idUmowyNajmu,
            int page,
            int pageSize,
            CancellationToken ct = default)
        {
            var currentPage = page <= 0 ? 1 : page;
            var currentPageSize = pageSize <= 0 ? 20 : Math.Min(pageSize, 200);
            var url = $"{BaseUrl}/lookup?page={currentPage}&pageSize={currentPageSize}";

            if (!string.IsNullOrWhiteSpace(q))
                url += $"&q={Uri.EscapeDataString(q)}";

            if (idUmowyNajmu.HasValue && idUmowyNajmu.Value != Guid.Empty)
                url += $"&idUmowyNajmu={idUmowyNajmu.Value}";

            return await _http.GetFromJsonAsync<PagedResultDto<PrzedmiotNajmuLookupDto>>(url, ct)
                   ?? new PagedResultDto<PrzedmiotNajmuLookupDto>();
        }


        public async Task<PrzedmiotNajmuDto> CreateAsync(CreatePrzedmiotNajmuRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, request, ct);
            resp.EnsureSuccessStatusCode();

            var dto = await resp.Content.ReadFromJsonAsync<PrzedmiotNajmuDto>(cancellationToken: ct);
            return dto ?? throw new InvalidOperationException("API zwróciło pustą odpowiedź (PrzedmiotNajmuDto = null).");
        }

        public async Task<bool> UpdateAsync(long id, UpdatePrzedmiotNajmuRequest request, CancellationToken ct = default)
        {
            var resp = await _http.PutAsJsonAsync($"{BaseUrl}/{id}", request, ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return false;

            resp.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> DeleteAsync(long id, CancellationToken ct = default)
        {
            var resp = await _http.DeleteAsync($"{BaseUrl}/{id}", ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) return false;

            resp.EnsureSuccessStatusCode();
            return true;
        }
    }
}