using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Contracts.DTOs.Media;
using Microsoft.Extensions.Logging;

namespace CastlePlus2.Client.Services.Media
{
    public sealed class RodzajeMediowService : IRodzajeMediowService
    {
        private const string BasePath = "api/media/RodzajeMediow";
        private readonly HttpClient _http;
        private readonly ILogger<RodzajeMediowService> _logger;

        public RodzajeMediowService(HttpClient http, ILogger<RodzajeMediowService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<List<RodzajMediumDto>> GetAllAsync(CancellationToken ct = default)
        {
            try
            {
                return await _http.GetFromJsonAsync<List<RodzajMediumDto>>(BasePath, ct) ?? new List<RodzajMediumDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllAsync failed.");
                throw;
            }
        }

        public async Task<RodzajMediumDto?> GetByIdAsync(string kodRodzaju, CancellationToken ct = default)
        {
            var kod = (kodRodzaju ?? string.Empty).Trim();
            if (kod.Length == 0) return null;

            try
            {
                var resp = await _http.GetAsync($"{BasePath}/{Uri.EscapeDataString(kod)}", ct);
                if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
                resp.EnsureSuccessStatusCode();
                return await resp.Content.ReadFromJsonAsync<RodzajMediumDto>(cancellationToken: ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetByIdAsync failed. KodRodzaju={KodRodzaju}", kod);
                throw;
            }
        }

        public async Task<RodzajMediumDto> CreateAsync(CreateRodzajMediumRequest request, CancellationToken ct = default)
        {
            try
            {
                var resp = await _http.PostAsJsonAsync(BasePath, request, ct);
                resp.EnsureSuccessStatusCode();
                return (await resp.Content.ReadFromJsonAsync<RodzajMediumDto>(cancellationToken: ct))!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateAsync failed.");
                throw;
            }
        }

        public async Task<RodzajMediumDto?> UpdateAsync(string kodRodzaju, UpdateRodzajMediumRequest request, CancellationToken ct = default)
        {
            var kod = (kodRodzaju ?? string.Empty).Trim();
            if (kod.Length == 0) return null;

            try
            {
                var resp = await _http.PutAsJsonAsync($"{BasePath}/{Uri.EscapeDataString(kod)}", request, ct);
                if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
                resp.EnsureSuccessStatusCode();
                return await resp.Content.ReadFromJsonAsync<RodzajMediumDto>(cancellationToken: ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateAsync failed. KodRodzaju={KodRodzaju}", kod);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(string kodRodzaju, CancellationToken ct = default)
        {
            var kod = (kodRodzaju ?? string.Empty).Trim();
            if (kod.Length == 0) return false;

            try
            {
                var resp = await _http.DeleteAsync($"{BasePath}/{Uri.EscapeDataString(kod)}", ct);
                if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return false;
                resp.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteAsync failed. KodRodzaju={KodRodzaju}", kod);
                throw;
            }
        }
    }
}
