using System;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Auth;

namespace CastlePlus2.Application.Interfaces.Auth
{
    public interface IUzytkownikAuthRepository
    {
        Task<Uzytkownik?> FindByLoginOrEmailAsync(string loginOrEmail, CancellationToken ct);
        Task<Uzytkownik?> FindByIdAsync(int idUzytkownika, CancellationToken ct);
        Task<string[]> GetRoleCodesAsync(int idUzytkownika, CancellationToken ct);
        Task UpdateLastLoginAsync(int idUzytkownika, DateTime utcNow, CancellationToken ct);
    }
}
