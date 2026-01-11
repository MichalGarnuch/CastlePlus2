using System.Threading.Tasks;

namespace CastlePlus2.Client.Services.Auth.Storage;

public interface IAccessTokenStore
{
    Task<AccessTokenPair?> GetAsync();
    Task SetAsync(AccessTokenPair pair);
    Task ClearAsync();
}