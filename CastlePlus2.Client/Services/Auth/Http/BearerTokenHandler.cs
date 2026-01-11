using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Client.Services.Auth.Storage;

namespace CastlePlus2.Client.Services.Auth.Http;

public class BearerTokenHandler : DelegatingHandler
{
    private readonly IAccessTokenStore _tokenStore;

    public BearerTokenHandler(IAccessTokenStore tokenStore)
    {
        _tokenStore = tokenStore;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.Headers.Authorization == null)
        {
            var tokens = await _tokenStore.GetAsync();
            if (!string.IsNullOrWhiteSpace(tokens?.AccessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}