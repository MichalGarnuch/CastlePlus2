using System.Text.Json;
using System.Threading.Tasks;
using CastlePlus2.Client.Services.Auth.Storage;
using Microsoft.Maui.Storage;

namespace CastlePlus2.Mobile.Services.Auth;

public class SecureStorageTokenStore : IAccessTokenStore
{
    private const string StorageKey = "castleplus2.tokens";

    public async Task<AccessTokenPair?> GetAsync()
    {
        var json = await SecureStorage.Default.GetAsync(StorageKey);
        if (string.IsNullOrWhiteSpace(json))
        {
            return null;
        }

        return JsonSerializer.Deserialize<AccessTokenPair>(json);
    }

    public Task SetAsync(AccessTokenPair pair)
    {
        var json = JsonSerializer.Serialize(pair);
        return SecureStorage.Default.SetAsync(StorageKey, json);
    }

    public Task ClearAsync()
    {
        SecureStorage.Default.Remove(StorageKey);
        return Task.CompletedTask;
    }
}