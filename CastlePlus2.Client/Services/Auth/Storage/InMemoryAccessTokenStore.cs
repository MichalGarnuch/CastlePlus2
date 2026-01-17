using System.Threading.Tasks;

namespace CastlePlus2.Client.Services.Auth.Storage;

public sealed class InMemoryAccessTokenStore : IAccessTokenStore
{
    private AccessTokenPair? _pair;

    public Task<AccessTokenPair?> GetAsync()
        => Task.FromResult(_pair);

    public Task SetAsync(AccessTokenPair pair)
    {
        _pair = pair;
        return Task.CompletedTask;
    }

    public Task ClearAsync()
    {
        _pair = null;
        return Task.CompletedTask;
    }
}
