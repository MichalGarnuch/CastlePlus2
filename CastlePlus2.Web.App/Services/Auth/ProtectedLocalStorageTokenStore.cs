using System;
using System.Threading.Tasks;
using CastlePlus2.Client.Services.Auth.Storage;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CastlePlus2.Web.App.Services.Auth;

public sealed class ProtectedLocalStorageTokenStore : IAccessTokenStore
{
    private const string StorageKey = "castleplus2.tokens";
    private readonly ProtectedLocalStorage _storage;

    public ProtectedLocalStorageTokenStore(ProtectedLocalStorage storage)
    {
        _storage = storage;
    }

    public async Task<AccessTokenPair?> GetAsync()
    {
        try
        {
            var result = await _storage.GetAsync<AccessTokenPair>(StorageKey);
            return result.Success ? result.Value : null;
        }
        catch (InvalidOperationException)
        {
            // SSR/prerender: JS interop nie działa -> traktujemy jak brak tokenów
            return null;
        }
    }

    public async Task SetAsync(AccessTokenPair pair)
    {
        try
        {
            await _storage.SetAsync(StorageKey, pair);
        }
        catch (InvalidOperationException)
        {
            // SSR/prerender – ignorujemy
        }
    }

    public async Task ClearAsync()
    {
        try
        {
            await _storage.DeleteAsync(StorageKey);
        }
        catch (InvalidOperationException)
        {
            // SSR/prerender – ignorujemy
        }
    }
}
