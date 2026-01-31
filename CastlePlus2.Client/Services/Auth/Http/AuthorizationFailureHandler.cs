using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Client.Services.Auth.State;
using CastlePlus2.Client.Services.Auth.Storage;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace CastlePlus2.Client.Services.Auth.Http;

public sealed class AuthorizationFailureHandler : DelegatingHandler
{
    private readonly IAccessTokenStore _accessTokenStore;
    private readonly CustomAuthStateProvider _authStateProvider;
    private readonly NavigationManager _navigationManager;
    private readonly ILogger<AuthorizationFailureHandler> _logger;

    public AuthorizationFailureHandler(
        IAccessTokenStore accessTokenStore,
        CustomAuthStateProvider authStateProvider,
        NavigationManager navigationManager,
        ILogger<AuthorizationFailureHandler> logger)
    {
        _accessTokenStore = accessTokenStore;
        _authStateProvider = authStateProvider;
        _navigationManager = navigationManager;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage response;

        try
        {
            response = await base.SendAsync(request, cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, "Błąd połączenia HTTP dla {Method} {Url}.", request.Method, request.RequestUri);
            throw;
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _logger.LogInformation("Wylogowanie po 401 dla {Method} {Url}.", request.Method, request.RequestUri);

            await _accessTokenStore.ClearAsync();
            await _authStateProvider.MarkUserAsLoggedOutAsync();

            if (!_navigationManager.Uri.Contains("/auth/login", StringComparison.OrdinalIgnoreCase))
            {
                _navigationManager.NavigateTo("/auth/login", forceLoad: true);
            }
        }

        return response;
    }
}