using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Contracts.DTOs.Media;
using CastlePlus2.Contracts.Requests.Media;

namespace CastlePlus2.Client.Services.Media
{
    public class OdczytyService : IOdczytyService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/media/odczyty";

        public OdczytyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<OdczytDto>> GetAllAsync(CancellationToken ct = default)
        {
            return await _httpClient.GetFromJsonAsync<List<OdczytDto>>(BaseUrl, ct) ?? new List<OdczytDto>();
        }

        public async Task<OdczytDto?> GetByIdAsync(long idOdczytu, CancellationToken ct = default)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{idOdczytu}", ct);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<OdczytDto>(cancellationToken: ct);
        }

        public async Task<OdczytDto> CreateAsync(CreateOdczytRequest request, CancellationToken ct = default)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, request, ct);
            response.EnsureSuccessStatusCode();

            var dto = await response.Content.ReadFromJsonAsync<OdczytDto>(cancellationToken: ct);
            return dto!;
        }

        public async Task<OdczytDto?> UpdateAsync(long idOdczytu, UpdateOdczytRequest request, CancellationToken ct = default)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{idOdczytu}", request, ct);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<OdczytDto>(cancellationToken: ct);
        }

        public async Task<bool> DeleteAsync(long idOdczytu, CancellationToken ct = default)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{idOdczytu}", ct);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return false;

            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
