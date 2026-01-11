using System.Threading.Tasks;
using CastlePlus2.Contracts.DTOs.Auth;

namespace CastlePlus2.Client.Services.Auth;

public interface IAuthService
{
    Task<AuthTokensDto> LoginAsync(string loginOrEmail, string password, string? deviceInfo);
    Task<AuthTokensDto> RefreshAsync(string? deviceInfo);
    Task<CurrentUserDto> GetMeAsync();
    Task LogoutAsync();
}