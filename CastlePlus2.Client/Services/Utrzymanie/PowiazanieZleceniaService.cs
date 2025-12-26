using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using CastlePlus2.Contracts.Requests.Utrzymanie;
using Microsoft.Extensions.Logging;

namespace CastlePlus2.Client.Services.Utrzymanie
{
    public sealed class PowiazaniaZleceniaService : IPowiazaniaZleceniaService
    {
        private const string BasePath = "api/PowiazaniaZlecenia";
        private readonly HttpClient _http;
        private readonly ILogger<PowiazaniaZleceniaService> _logger;

        public PowiazaniaZleceniaService(HttpClient http, ILogger<PowiazaniaZleceniaService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<List<PowiazanieZleceniaDto>> GetAllAsync(CancellationToken ct = default)
        {
            try
            {
                return await _http.GetFromJsonAsync<List<PowiazanieZleceniaDto>>(BasePath, ct) ?? new List<PowiazanieZleceniaDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllAsync failed.");
                throw;
            }
        }

        public async Task<PowiazanieZleceniaDto?> GetByIdAsync(long idPowiazania, CancellationToken ct = default)
        {
            if (idPowiazania <= 0) return null;

            try
            {
                var resp = await _http.GetAsync($"{BasePath}/{idPowiazania}", ct);
                if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
                resp.EnsureSuccessStatusCode();
                return await resp.Content.ReadFromJsonAsync<PowiazanieZleceniaDto>(cancellationToken: ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetByIdAsync failed. IdPowiazania={IdPowiazania}", idPowiazania);
                throw;
            }
        }

        public async Task<PowiazanieZleceniaDto> CreateAsync(CreatePowiazanieZleceniaRequest request, CancellationToken ct = default)
        {
            try
            {
                var resp = await _http.PostAsJsonAsync(BasePath, request, ct);
                resp.EnsureSuccessStatusCode();
                return (await resp.Content.ReadFromJsonAsync<PowiazanieZleceniaDto>(cancellationToken: ct))!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateAsync failed.");
                throw;
            }
        }

        public async Task<PowiazanieZleceniaDto?> UpdateAsync(long idPowiazania, UpdatePowiazanieZleceniaRequest request, CancellationToken ct = default)
        {
            if (idPowiazania <= 0) return null;

            try
            {
                var resp = await _http.PutAsJsonAsync($"{BasePath}/{idPowiazania}", request, ct);
                if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
                resp.EnsureSuccessStatusCode();
                return await resp.Content.ReadFromJsonAsync<PowiazanieZleceniaDto>(cancellationToken: ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateAsync failed. IdPowiazania={IdPowiazania}", idPowiazania);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(long idPowiazania, CancellationToken ct = default)
        {
            if (idPowiazania <= 0) return false;

            try
            {
                var resp = await _http.DeleteAsync($"{BasePath}/{idPowiazania}", ct);
                if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return false;
                resp.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteAsync failed. IdPowiazania={IdPowiazania}", idPowiazania);
                throw;
            }
        }
    }
}