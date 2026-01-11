using CastlePlus2.Domain.Entities.Auth;

namespace CastlePlus2.Application.Interfaces.Auth
{
    public interface IUzytkownikAuthRepository
    {
        Task<Uzytkownik?> FindByLoginOrEmailAsync(string loginOrEmail, CancellationToken ct);
        Task<Uzytkownik?> FindByIdAsync(int idUzytkownika, CancellationToken ct);
        Task<string[]> GetRoleCodesAsync(int idUzytkownika, CancellationToken ct);
        Task UpdateLastLoginAsync(int idUzytkownika, DateTime utcNow, CancellationToken ct);

        Task<bool> AnyUsersAsync(CancellationToken ct);
        Task<bool> LoginExistsAsync(string login, CancellationToken ct);
        Task<bool> EmailExistsAsync(string email, CancellationToken ct);

        Task<int> CreateUserAsync(Uzytkownik user, CancellationToken ct);
        Task<int?> GetRoleIdByCodeAsync(string roleCode, CancellationToken ct);
        Task AssignRoleAsync(int userId, int roleId, CancellationToken ct);
    }
}
