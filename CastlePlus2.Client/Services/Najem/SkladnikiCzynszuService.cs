using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;

namespace CastlePlus2.Client.Services.Najem
{
    public class SkladnikiCzynszuService : ISkladnikiCzynszuService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/najem/SkladnikiCzynszu";

        public SkladnikiCzynszuService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SkladnikCzynszuDto>> GetAllAsync(CancellationToken ct = default)
        {
            return await _httpClient.GetFromJsonAsync<List<SkladnikCzynszuDto>>(BaseUrl, ct) ?? new List<SkladnikCzynszuDto>();
        }

        public async Task<SkladnikCzynszuDto?> GetByIdAsync(long idSkladnikaCzynszu, CancellationToken ct = default)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{idSkladnikaCzynszu}", ct);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<SkladnikCzynszuDto>(cancellationToken: ct);
        }

        public async Task<SkladnikCzynszuDto> CreateAsync(CreateSkladnikCzynszuRequest request, CancellationToken ct = default)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, request, ct);
            response.EnsureSuccessStatusCode();

            var dto = await response.Content.ReadFromJsonAsync<SkladnikCzynszuDto>(cancellationToken: ct);
            return dto!;
        }

        public async Task<SkladnikCzynszuDto?> UpdateAsync(long idSkladnikaCzynszu, UpdateSkladnikCzynszuRequest request, CancellationToken ct = default)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{idSkladnikaCzynszu}", request, ct);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<SkladnikCzynszuDto>(cancellationToken: ct);
        }

        public async Task<bool> DeleteAsync(long idSkladnikaCzynszu, CancellationToken ct = default)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{idSkladnikaCzynszu}", ct);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return false;

            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}