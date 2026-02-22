using System.Threading.Tasks;
using CastlePlus2.Contracts.DTOs.Auth;

namespace CastlePlus2.Client.Services.Auth;

public interface IAuthService
{
    Task<AuthTokensDto> LoginAsync(string loginOrEmail, string password, string? deviceInfo);
    Task<AuthTokensDto> RegisterAsync(string login, string? email, string password, string? deviceInfo);
    Task<AuthTokensDto> RefreshAsync(string? deviceInfo);
    Task<CurrentUserDto> GetMeAsync();
    Task ChangePasswordAsync(string currentPassword, string newPassword, string confirmNewPassword);
    Task LogoutAsync();
}
