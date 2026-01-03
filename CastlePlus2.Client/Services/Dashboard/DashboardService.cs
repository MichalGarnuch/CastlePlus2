using System.Net.Http.Json;
using CastlePlus2.Contracts.DTOs.Dashboard;

namespace CastlePlus2.Client.Services.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "api/dashboard/najem";

        public DashboardService(HttpClient http)
        {
            _http = http;
        }

        public async Task<NajemDashboardDto> GetNajemDashboardAsync(CancellationToken ct = default)
        {
            return await _http.GetFromJsonAsync<NajemDashboardDto>(BaseUrl, ct)
                   ?? new NajemDashboardDto();
        }
    }
}