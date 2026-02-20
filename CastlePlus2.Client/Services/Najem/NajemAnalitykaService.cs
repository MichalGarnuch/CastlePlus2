using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;
using System.Net.Http.Json;

namespace CastlePlus2.Client.Services.Najem
{
    public class NajemAnalitykaService : INajemAnalitykaService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "api/najem/analityka";

        public NajemAnalitykaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IReadOnlyList<OblozenieLokaluDto>> GetOblozenieLokaliUtcDzisAsync(CancellationToken ct = default)
            => await _http.GetFromJsonAsync<List<OblozenieLokaluDto>>($"{BaseUrl}/oblozenie-utc-dzis", ct)
               ?? new List<OblozenieLokaluDto>();

        public async Task<IReadOnlyList<RaportNajmuZaMiesiacRowDto>> GetRaportNajmuZaMiesiacAsync(GetRaportNajmuZaMiesiacRequest request, CancellationToken ct = default)
        {
            var response = await _http.PostAsJsonAsync($"{BaseUrl}/raport-miesiac", request, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<RaportNajmuZaMiesiacRowDto>>(cancellationToken: ct)
                ?? new List<RaportNajmuZaMiesiacRowDto>();
        }
    }
}