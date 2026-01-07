using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Dashboard;

namespace CastlePlus2.Client.Services.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "api/dashboard/najem";
        private const string V1BaseUrl = "api/dashboard/v1/najem";

        public DashboardService(HttpClient http)
        {
            _http = http;
        }

        public async Task<NajemDashboardDto> GetNajemDashboardAsync(
            int zakresDni = 30,
            CancellationToken ct = default)
        {
            var url = $"{BaseUrl}?zakresDni={zakresDni}";

            return await _http.GetFromJsonAsync<NajemDashboardDto>(url, ct)
                   ?? new NajemDashboardDto();
        }

        public async Task<DashboardV1NajemDto> GetDashboardV1NajemAsync(
            CancellationToken ct = default)
        {
            return await _http.GetFromJsonAsync<DashboardV1NajemDto>(V1BaseUrl, ct)
                   ?? new DashboardV1NajemDto();
        }
    }
}
