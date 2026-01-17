using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Client.Services.Auth.Storage;

namespace CastlePlus2.Client.Services.Auth.Http;

public sealed class BearerTokenHandler : DelegatingHandler
{
    private readonly IAccessTokenStore _accessTokenStore;

    public BearerTokenHandler(IAccessTokenStore accessTokenStore)
    {
        _accessTokenStore = accessTokenStore;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Nie nadpisuj ręcznie ustawionego Authorization
        if (request.Headers.Authorization is null)
        {
            var pair = await _accessTokenStore.GetAsync();
            var accessToken = pair?.AccessToken;

            if (!string.IsNullOrWhiteSpace(accessToken))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
