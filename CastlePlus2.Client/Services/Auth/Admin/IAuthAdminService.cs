using System.Threading.Tasks;
using CastlePlus2.Contracts.DTOs.Auth;

namespace CastlePlus2.Client.Services.Auth.Admin;

public interface IAuthAdminService
{
    Task<AdminUserDto[]> GetUsersAsync();
    Task<RoleDto[]> GetRolesAsync();
    Task SetUserRolesAsync(int userId, string[] roleCodes);
    Task CreateUserAsync(string login, string email, string[] roleCodes);
}
