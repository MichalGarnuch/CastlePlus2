using System;
using CastlePlus2.Application.Interfaces.Auth;
using CastlePlus2.Domain.Entities.Auth;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Auth
{
    /// <summary>
    /// Repozytorium EF Core dla operacji autoryzacyjnych użytkownika.
    /// </summary>
    public class UzytkownikAuthRepository : IUzytkownikAuthRepository
    {
        private readonly CastlePlus2DbContext _dbContext;

        public UzytkownikAuthRepository(CastlePlus2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Uzytkownik?> FindByLoginOrEmailAsync(string loginOrEmail, CancellationToken ct)
        {
            return await _dbContext.Uzytkownicy
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Login == loginOrEmail || x.Email == loginOrEmail, ct);
        }

        public async Task<string[]> GetRoleCodesAsync(int idUzytkownika, CancellationToken ct)
        {
            return await _dbContext.UzytkownikRole
                .AsNoTracking()
                .Where(x => x.IdUzytkownika == idUzytkownika)
                .Select(x => x.Rola.Kod)
                .Distinct()
                .ToArrayAsync(ct);
        }

        public async Task UpdateLastLoginAsync(int idUzytkownika, DateTime utcNow, CancellationToken ct)
        {
            var uzytkownik = await _dbContext.Uzytkownicy
                .FirstOrDefaultAsync(x => x.IdUzytkownika == idUzytkownika, ct);

            if (uzytkownik == null)
            {
                return;
            }

            uzytkownik.OstatnieLogowanieUtc = utcNow;

            await _dbContext.SaveChangesAsync(ct);
        }
    }
}