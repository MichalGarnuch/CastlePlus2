using System.Security.Claims;
using System.Threading.Tasks;
using CastlePlus2.Client.Services.Auth;
using CastlePlus2.Client.Services.Auth.Storage;
using Microsoft.AspNetCore.Components.Authorization;

namespace CastlePlus2.Client.Services.Auth.State;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IAccessTokenStore _tokenStore;
    private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());

    public CustomAuthStateProvider(IAccessTokenStore tokenStore)
    {
        _tokenStore = tokenStore;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (_currentUser.Identity?.IsAuthenticated == true)
        {
            return new AuthenticationState(_currentUser);
        }

        var tokens = await _tokenStore.GetAsync();
        if (!string.IsNullOrWhiteSpace(tokens?.AccessToken))
        {
            _currentUser = BuildPrincipal(tokens.AccessToken);
        }

        return new AuthenticationState(_currentUser);
    }

    public async Task MarkUserAsAuthenticatedFromTokenAsync()
    {
        var tokens = await _tokenStore.GetAsync();
        _currentUser = !string.IsNullOrWhiteSpace(tokens?.AccessToken)
            ? BuildPrincipal(tokens.AccessToken)
            : new ClaimsPrincipal(new ClaimsIdentity());

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
    }

    public Task MarkUserAsLoggedOutAsync()
    {
        _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        return Task.CompletedTask;
    }

    private static ClaimsPrincipal BuildPrincipal(string accessToken)
    {
        var claims = JwtClaimsParser.ParseClaimsFromJwt(accessToken);
        var identity = new ClaimsIdentity(claims, "jwt");
        return new ClaimsPrincipal(identity);
    }
}