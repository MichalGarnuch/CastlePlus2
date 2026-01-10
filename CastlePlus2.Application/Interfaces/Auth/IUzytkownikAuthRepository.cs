using System;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Auth;

namespace CastlePlus2.Application.Interfaces.Auth
{
    /// <summary>
    /// Interfejs repozytorium dla odczytów użytkownika w module auth.
    /// </summary>
    public interface IUzytkownikAuthRepository
    {
        Task<Uzytkownik?> FindByLoginOrEmailAsync(string loginOrEmail, CancellationToken ct);
        Task<string[]> GetRoleCodesAsync(int idUzytkownika, CancellationToken ct);
        Task UpdateLastLoginAsync(int idUzytkownika, DateTime utcNow, CancellationToken ct);
    }
}