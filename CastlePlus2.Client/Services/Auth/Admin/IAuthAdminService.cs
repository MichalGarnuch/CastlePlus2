using System.Threading.Tasks;
using CastlePlus2.Contracts.DTOs.Auth;

namespace CastlePlus2.Client.Services.Auth.Admin;

public interface IAuthAdminService
{
    Task<AdminUserDto[]> GetUsersAsync();
    Task<RoleDto[]> GetRolesAsync();

    Task SetUserRolesAsync(int userId, string[] roleCodes);

    // Docelowa metoda: admin tworzy konto i nadaje hasło od razu
    Task CreateUserAsync(string login, string email, string password, string confirmPassword, string[] roleCodes);
}