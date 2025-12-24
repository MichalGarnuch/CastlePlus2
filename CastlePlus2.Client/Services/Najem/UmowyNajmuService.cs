using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Contracts.DTOs.Najem;

namespace CastlePlus2.Client.Services.Najem
{
    public class UmowyNajmuService : IUmowyNajmuService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/najem/UmowyNajmu";

        public UmowyNajmuService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UmowaNajmuDto>> GetAllAsync(CancellationToken ct = default)
        {
            return await _httpClient.GetFromJsonAsync<List<UmowaNajmuDto>>(BaseUrl, ct) ?? new List<UmowaNajmuDto>();
        }

        public async Task<UmowaNajmuDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}", ct);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UmowaNajmuDto>(cancellationToken: ct);
        }
    }
}