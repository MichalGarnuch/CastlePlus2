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

        public async Task<Uzytkownik?> FindByLoginOrEmailAsync(string loginOrEmail, CancellationToken ct)
        {
            return await _dbContext.Uzytkownicy
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Login == loginOrEmail || x.Email == loginOrEmail, ct);
        }

        public async Task<Uzytkownik?> FindByIdAsync(int idUzytkownika, CancellationToken ct)
        {
            return await _dbContext.Uzytkownicy
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

        public Task<bool> AnyUsersAsync(CancellationToken ct)
        {
            return _dbContext.Uzytkownicy.AnyAsync(ct);
        }

        public Task<bool> LoginExistsAsync(string login, CancellationToken ct)
        {
            return _dbContext.Uzytkownicy.AnyAsync(x => x.Login == login, ct);
        }

        public Task<bool> EmailExistsAsync(string email, CancellationToken ct)
        {
            return _dbContext.Uzytkownicy.AnyAsync(x => x.Email == email, ct);
        }

        public async Task<int> CreateUserAsync(Uzytkownik user, CancellationToken ct)
        {
            _dbContext.Uzytkownicy.Add(user);
            await _dbContext.SaveChangesAsync(ct);
            return user.IdUzytkownika;
        }

        public Task<int?> GetRoleIdByCodeAsync(string roleCode, CancellationToken ct)
        {
            return _dbContext.Role
                .AsNoTracking()
                .Where(x => x.Kod == roleCode)
                .Select(x => (int?)x.IdRoli)
                .FirstOrDefaultAsync(ct);
        }

        public async Task AssignRoleAsync(int userId, int roleId, CancellationToken ct)
        {
            _dbContext.UzytkownikRole.Add(new UzytkownikRola
            {
                IdUzytkownika = userId,
                IdRoli = roleId
            });

            await _dbContext.SaveChangesAsync(ct);
        }
    }
}
