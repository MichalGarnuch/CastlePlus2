using CastlePlus2.Application.Interfaces.Auth;
using CastlePlus2.Contracts.DTOs.Auth;
using CastlePlus2.Domain.Entities.Auth;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            var normalized = (loginOrEmail ?? string.Empty).Trim();
            return _dbContext.Uzytkownicy
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Login == normalized || x.Email == normalized, ct);
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

            if (uzytkownik is null)
                return;

            uzytkownik.OstatnieLogowanieUtc = utcNow;
            await _dbContext.SaveChangesAsync(ct);
        }

        public Task<bool> AnyUsersAsync(CancellationToken ct)
            => _dbContext.Uzytkownicy.AnyAsync(ct);

        public Task<bool> LoginExistsAsync(string login, CancellationToken ct)
            => _dbContext.Uzytkownicy.AnyAsync(x => x.Login == login, ct);

        public Task<bool> EmailExistsAsync(string email, CancellationToken ct)
            => _dbContext.Uzytkownicy.AnyAsync(x => x.Email == email, ct);

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

        public async Task<AdminUserDto[]> GetUsersWithRolesAsync(CancellationToken ct)
        {
            var users = await _dbContext.Uzytkownicy
                .AsNoTracking()
                .Select(u => new
                {
                    u.IdUzytkownika,
                    u.Login,
                    u.Email,
                    u.CzyAktywny,
                    u.CzyUsuniety,
                    RoleCodes = u.UzytkownikRole.Select(ur => ur.Rola.Kod)
                })
                .ToListAsync(ct);

            return users
                .Select(u => new AdminUserDto
                {
                    IdUzytkownika = u.IdUzytkownika,
                    Login = u.Login,
                    Email = u.Email,
                    CzyAktywny = u.CzyAktywny,
                    CzyUsuniety = u.CzyUsuniety,
                    RoleCodes = u.RoleCodes.Distinct(StringComparer.OrdinalIgnoreCase).ToArray()
                })
                .ToArray();
        }

        public Task<RoleDto[]> GetRolesAsync(CancellationToken ct)
        {
            return _dbContext.Role
                .AsNoTracking()
                .Select(r => new RoleDto
                {
                    IdRoli = r.IdRoli,
                    Kod = r.Kod,
                    Nazwa = r.Nazwa
                })
                .ToArrayAsync(ct);
        }

        public Task<bool> RoleExistsByCodeAsync(string code, CancellationToken ct)
            => _dbContext.Role.AnyAsync(r => r.Kod == code, ct);

        public async Task ReplaceUserRolesAsync(int userId, string[] roleCodes, CancellationToken ct)
        {
            var distinctRoleCodes = (roleCodes ?? Array.Empty<string>())
                .Where(code => !string.IsNullOrWhiteSpace(code))
                .Select(code => code.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            if (distinctRoleCodes.Length == 0)
                return;

            // Ważne: ExecutionStrategy + transakcja w środku
            var strategy = _dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);

                // Pobierz role pod wskazane kody (przy typowej kolacji SQL Server i tak będzie case-insensitive)
                var roles = await _dbContext.Role
                    .Where(r => distinctRoleCodes.Contains(r.Kod))
                    .Select(r => r.IdRoli)
                    .ToListAsync(ct);

                // Replace
                var existing = _dbContext.UzytkownikRole.Where(x => x.IdUzytkownika == userId);
                _dbContext.UzytkownikRole.RemoveRange(existing);

                foreach (var roleId in roles)
                {
                    _dbContext.UzytkownikRole.Add(new UzytkownikRola
                    {
                        IdUzytkownika = userId,
                        IdRoli = roleId
                    });
                }

                await _dbContext.SaveChangesAsync(ct);
                await transaction.CommitAsync(ct);
            });
        }

        public async Task<bool> SetUserActiveAsync(int userId, bool isActive, DateTime utcNow, CancellationToken ct)
        {
            var user = await _dbContext.Uzytkownicy.FirstOrDefaultAsync(x => x.IdUzytkownika == userId, ct);
            if (user is null)
            {
                return false;
            }

            user.CzyAktywny = isActive;
            user.DataModyfikacjiUtc = utcNow;
            _dbContext.Uzytkownicy.Update(user);
            await _dbContext.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> SoftDeleteUserAsync(int userId, string deletedBy, DateTime utcNow, CancellationToken ct)
        {
            var user = await _dbContext.Uzytkownicy.FirstOrDefaultAsync(x => x.IdUzytkownika == userId, ct);
            if (user is null)
            {
                return false;
            }

            user.CzyUsuniety = true;
            user.UsunietoUtc = utcNow;
            user.UsunietoPrzez = deletedBy;
            user.DataModyfikacjiUtc = utcNow;
            _dbContext.Uzytkownicy.Update(user);
            await _dbContext.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> RestoreUserAsync(int userId, DateTime utcNow, CancellationToken ct)
        {
            var user = await _dbContext.Uzytkownicy.FirstOrDefaultAsync(x => x.IdUzytkownika == userId, ct);
            if (user is null)
            {
                return false;
            }

            user.CzyUsuniety = false;
            user.UsunietoUtc = null;
            user.UsunietoPrzez = null;

            // Zalecane: restore odblokowuje konto
            user.CzyAktywny = true;

            user.DataModyfikacjiUtc = utcNow;
            _dbContext.Uzytkownicy.Update(user);
            await _dbContext.SaveChangesAsync(ct);
            return true;
        }

        public async Task UpdatePasswordAsync(int userId, string passwordHash, DateTime utcNow, CancellationToken ct)
        {
            var user = await _dbContext.Uzytkownicy.FirstOrDefaultAsync(x => x.IdUzytkownika == userId, ct);
            if (user is null)
            {
                return;
            }

            user.HasloHash = passwordHash;
            user.DataModyfikacjiUtc = utcNow;
            _dbContext.Uzytkownicy.Update(user);
            await _dbContext.SaveChangesAsync(ct);
        }

    }
}
