using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CastlePlus2.Contracts.DTOs.Auth;
using CastlePlus2.Contracts.Requests.Auth;

namespace CastlePlus2.Client.Services.Auth.RequestAccess
{
    public sealed class RequestAccessAdminService : IRequestAccessAdminService
    {
        private readonly HttpClient _httpClient;

        public RequestAccessAdminService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RequestAccessDto[]> GetRequestsAsync(string status)
        {
            var response = await _httpClient.GetFromJsonAsync<RequestAccessDto[]>($"api/admin/request-access?status={Uri.EscapeDataString(status)}");
            if (response is null)
            {
                throw new InvalidOperationException("Brak listy zgłoszeń w odpowiedzi.");
            }

            return response;
        }

        public async Task ApproveAsync(int requestId, string login, string email, string[] roleCodes)
        {
            var request = new ApproveRequestAccessRequest
            {
                Login = login,
                Email = email,
                RoleCodes = roleCodes ?? Array.Empty<string>()
            };

            var response = await _httpClient.PostAsJsonAsync($"api/admin/request-access/{requestId}/approve", request);
            response.EnsureSuccessStatusCode();
        }

        public async Task RejectAsync(int requestId, string? reason)
        {
            var request = new RejectRequestAccessRequest
            {
                Reason = reason
            };

            var response = await _httpClient.PostAsJsonAsync($"api/admin/request-access/{requestId}/reject", request);
            response.EnsureSuccessStatusCode();
        }
    }
}