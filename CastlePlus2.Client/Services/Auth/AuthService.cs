using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using CastlePlus2.Client.Services.Auth.Storage;
using CastlePlus2.Contracts.DTOs.Auth;
using CastlePlus2.Contracts.Requests.Auth;
using Microsoft.Extensions.Logging;

namespace CastlePlus2.Client.Services.Auth;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;
    private readonly IAccessTokenStore _tokenStore;
    private readonly ILogger<AuthService> _logger;

    public AuthService(HttpClient http, IAccessTokenStore tokenStore, ILogger<AuthService> logger)
    {
        _http = http;
        _tokenStore = tokenStore;
        _logger = logger;
    }

    public async Task<AuthTokensDto> LoginAsync(string loginOrEmail, string password, string? deviceInfo)
    {
        var request = new LoginRequest
        {
            LoginOrEmail = loginOrEmail,
            Password = password,
            DeviceInfo = deviceInfo
        };

        var response = await _http.PostAsJsonAsync("api/auth/login", request);
        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            _logger.LogWarning("Login failed: {StatusCode}. Body: {Body}", response.StatusCode, body);
            throw new InvalidOperationException(TryExtractProblemMessage(body));
        }

        var tokens = await response.Content.ReadFromJsonAsync<AuthTokensDto>()
                     ?? throw new InvalidOperationException("Brak tokenów w odpowiedzi.");

        await _tokenStore.SetAsync(ToAccessTokenPair(tokens));
        return tokens;
    }

    public async Task<AuthTokensDto> RegisterAsync(string login, string? email, string password, string? deviceInfo)
    {
        var request = new RegisterRequest
        {
            Login = login,
            Email = string.IsNullOrWhiteSpace(email) ? null : email,
            Password = password,
            DeviceInfo = deviceInfo
        };

        var response = await _http.PostAsJsonAsync("api/auth/register", request);
        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            _logger.LogWarning("Register failed: {StatusCode}. Body: {Body}", response.StatusCode, body);
            throw new InvalidOperationException(TryExtractProblemMessage(body));
        }

        var tokens = await response.Content.ReadFromJsonAsync<AuthTokensDto>()
                     ?? throw new InvalidOperationException("Brak tokenów w odpowiedzi.");

        await _tokenStore.SetAsync(ToAccessTokenPair(tokens));
        return tokens;
    }

    public async Task<AuthTokensDto> RefreshAsync(string? deviceInfo)
    {
        var tokens = await _tokenStore.GetAsync();
        if (string.IsNullOrWhiteSpace(tokens?.RefreshToken))
        {
            throw new InvalidOperationException("Brak refresh tokena w magazynie.");
        }

        var request = new RefreshRequest
        {
            RefreshToken = tokens.RefreshToken,
            DeviceInfo = deviceInfo
        };

        var response = await _http.PostAsJsonAsync("api/auth/refresh", request);
        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            _logger.LogWarning("Refresh failed: {StatusCode}. Body: {Body}", response.StatusCode, body);
            throw new InvalidOperationException(TryExtractProblemMessage(body));
        }

        var refreshedTokens = await response.Content.ReadFromJsonAsync<AuthTokensDto>()
                             ?? throw new InvalidOperationException("Brak tokenów w odpowiedzi.");

        await _tokenStore.SetAsync(ToAccessTokenPair(refreshedTokens));
        return refreshedTokens;
    }

    public async Task<CurrentUserDto> GetMeAsync()
    {
        var response = await _http.GetAsync("api/auth/me");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<CurrentUserDto>()
               ?? throw new InvalidOperationException("Brak danych użytkownika w odpowiedzi.");
    }

    public async Task ChangePasswordAsync(string currentPassword, string newPassword, string confirmNewPassword)
    {
        var request = new ChangePasswordRequest
        {
            CurrentPassword = currentPassword,
            NewPassword = newPassword,
            ConfirmNewPassword = confirmNewPassword
        };

        var response = await _http.PostAsJsonAsync("api/auth/change-password", request);
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var body = await response.Content.ReadAsStringAsync();
        _logger.LogWarning("ChangePassword failed: {StatusCode}. Body: {Body}", response.StatusCode, body);
        throw new InvalidOperationException(TryExtractProblemMessage(body));
    }

    public Task LogoutAsync()
    {
        return _tokenStore.ClearAsync();
    }

    private static AccessTokenPair ToAccessTokenPair(AuthTokensDto tokens)
    {
        return new AccessTokenPair
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken,
            AccessTokenExpiresAtUtc = tokens.AccessTokenExpiresAtUtc
        };
    }

    private static string TryExtractProblemMessage(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            return "Brak treści odpowiedzi.";
        }

        try
        {
            using var doc = JsonDocument.Parse(body);
            var root = doc.RootElement;
            if (root.ValueKind != JsonValueKind.Object)
            {
                return body;
            }

            string? title = root.TryGetProperty("title", out var titleEl) && titleEl.ValueKind == JsonValueKind.String
                ? titleEl.GetString()
                : null;
            string? detail = root.TryGetProperty("detail", out var detailEl) && detailEl.ValueKind == JsonValueKind.String
                ? detailEl.GetString()
                : null;

            if (!string.IsNullOrWhiteSpace(title) || !string.IsNullOrWhiteSpace(detail))
            {
                return $"{title} {detail}".Trim();
            }
        }
        catch
        {
            // ignored
        }

        return body;
    }
}
