using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Auth;
using CastlePlus2.Domain.Entities.Auth;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Auth
{
    public sealed class UzytkownikAuthRepository : IUzytkownikAuthRepository
    {
        private readonly CastlePlus2DbContext _dbContext;

        public UzytkownikAuthRepository(CastlePlus2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Uzytkownik?> FindByLoginOrEmailAsync(string loginOrEmail, CancellationToken ct)
        {
            return _dbContext.Uzytkownicy
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Login == loginOrEmail || x.Email == loginOrEmail, ct);
        }

        public Task<Uzytkownik?> FindByIdAsync(int idUzytkownika, CancellationToken ct)
        {
            return _dbContext.Uzytkownicy
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdUzytkownika == idUzytkownika, ct);
        }

        public Task<string[]> GetRoleCodesAsync(int idUzytkownika, CancellationToken ct)
        {
            return _dbContext.UzytkownikRole
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
                return;

            uzytkownik.OstatnieLogowanieUtc = utcNow;
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}
