using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using CastlePlus2.Contracts.DTOs.Auth;
using CastlePlus2.Contracts.Requests.Auth;

namespace CastlePlus2.Client.Services.Auth.Admin;

public sealed class AuthAdminService : IAuthAdminService
{
    private readonly HttpClient _httpClient;

    public AuthAdminService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<AdminUserDto[]> GetUsersAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<AdminUserDto[]>("api/auth/admin/users");
        if (response is null)
        {
            throw new InvalidOperationException("Brak listy użytkowników w odpowiedzi.");
        }

        return response;
    }

    public async Task<RoleDto[]> GetRolesAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<RoleDto[]>("api/auth/admin/roles");
        if (response is null)
        {
            throw new InvalidOperationException("Brak listy ról w odpowiedzi.");
        }

        return response;
    }

    public async Task SetUserRolesAsync(int userId, string[] roleCodes)
    {
        var request = new SetUserRolesRequest
        {
            RoleCodes = roleCodes ?? Array.Empty<string>()
        };

        var response = await _httpClient.PutAsJsonAsync($"api/auth/admin/users/{userId}/roles", request);

        if (response.IsSuccessStatusCode)
        {
            return;
        }

        // zamiast EnsureSuccessStatusCode() -> czytelny błąd z body
        var body = await response.Content.ReadAsStringAsync();
        var message = TryExtractProblemMessage(body);

        throw new InvalidOperationException(
            $"Błąd zapisu ról: {(int)response.StatusCode} ({response.ReasonPhrase}). {message}");
    }

    public async Task CreateUserAsync(string login, string email, string[] roleCodes)
    {
        var request = new CreateUserRequest
        {
            Login = login,
            Email = email,
            RoleCodes = roleCodes ?? Array.Empty<string>()
        };

        var response = await _httpClient.PostAsJsonAsync("api/auth/admin/users", request);
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var body = await response.Content.ReadAsStringAsync();
        var message = TryExtractProblemMessage(body);
        throw new InvalidOperationException(
            $"Błąd tworzenia konta: {(int)response.StatusCode} ({response.ReasonPhrase}). {message}");
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
            if (doc.RootElement.ValueKind != JsonValueKind.Object)
            {
                return body;
            }

            var root = doc.RootElement;

            // ProblemDetails: title/detail
            string? title = null;
            string? detail = null;
            string? traceId = null;

            if (root.TryGetProperty("title", out var titleEl) && titleEl.ValueKind == JsonValueKind.String)
                title = titleEl.GetString();

            if (root.TryGetProperty("detail", out var detailEl) && detailEl.ValueKind == JsonValueKind.String)
                detail = detailEl.GetString();

            if (root.TryGetProperty("traceId", out var traceEl) && traceEl.ValueKind == JsonValueKind.String)
                traceId = traceEl.GetString();

            // ValidationProblemDetails: errors
            string? firstError = null;
            if (root.TryGetProperty("errors", out var errorsEl) && errorsEl.ValueKind == JsonValueKind.Object)
            {
                foreach (var prop in errorsEl.EnumerateObject())
                {
                    if (prop.Value.ValueKind == JsonValueKind.Array && prop.Value.GetArrayLength() > 0)
                    {
                        var first = prop.Value[0];
                        if (first.ValueKind == JsonValueKind.String)
                        {
                            firstError = $"{prop.Name}: {first.GetString()}";
                            break;
                        }
                    }
                }
            }

            var msg = "";

            if (!string.IsNullOrWhiteSpace(title) || !string.IsNullOrWhiteSpace(detail))
            {
                msg = $"{title} - {detail}".Trim(' ', '-');
            }

            if (!string.IsNullOrWhiteSpace(firstError))
            {
                msg = string.IsNullOrWhiteSpace(msg) ? firstError : $"{msg} | {firstError}";
            }

            if (!string.IsNullOrWhiteSpace(traceId))
            {
                msg = string.IsNullOrWhiteSpace(msg) ? $"TraceId={traceId}" : $"{msg} | TraceId={traceId}";
            }

            return string.IsNullOrWhiteSpace(msg) ? body : msg;
        }
        catch
        {
            return body;
        }
    }
}
