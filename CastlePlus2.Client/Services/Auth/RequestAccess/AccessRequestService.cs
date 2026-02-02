using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CastlePlus2.Contracts.Requests.Auth;

namespace CastlePlus2.Client.Services.Auth.RequestAccess
{
    public sealed class AccessRequestService : IAccessRequestService
    {
        private readonly HttpClient _httpClient;

        public AccessRequestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> CreateRequestAsync(CreateRequestAccessRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/request-access", request);
            response.EnsureSuccessStatusCode();

            var id = await response.Content.ReadFromJsonAsync<int?>();
            return id ?? 0;
        }

        public async Task ActivateAccountAsync(string token, string password, string confirmPassword)
        {
            var request = new ActivateAccountRequest
            {
                Token = token,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var response = await _httpClient.PostAsJsonAsync("api/auth/activate", request);
            response.EnsureSuccessStatusCode();
        }
    }
}
