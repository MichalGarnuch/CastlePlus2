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
    /// <summary>
    /// Repozytorium EF Core dla tokenów odświeżania.
    /// </summary>
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly CastlePlus2DbContext _dbContext;

        public RefreshTokenRepository(CastlePlus2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(RefreshToken token, CancellationToken ct)
        {
            await _dbContext.RefreshTokens.AddAsync(token, ct);
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task<RefreshToken?> FindByTokenHashAsync(byte[] tokenHash, CancellationToken ct)
        {
            return await _dbContext.RefreshTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TokenHash.SequenceEqual(tokenHash), ct);
        }

        public async Task RevokeAsync(long idRefreshToken, DateTime revokedAtUtc, CancellationToken ct)
        {
            var token = await _dbContext.RefreshTokens
                .FirstOrDefaultAsync(x => x.IdRefreshToken == idRefreshToken, ct);

            if (token == null)
            {
                return;
            }

            token.RevokedAtUtc = revokedAtUtc;

            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task RevokeAllForUserAsync(int userId, DateTime revokedAtUtc, CancellationToken ct)
        {
            var tokens = await _dbContext.RefreshTokens
                .Where(x => x.IdUzytkownika == userId && !x.RevokedAtUtc.HasValue)
                .ToListAsync(ct);

            if (tokens.Count == 0)
            {
                return;
            }

            foreach (var token in tokens)
            {
                token.RevokedAtUtc = revokedAtUtc;
            }

            await _dbContext.SaveChangesAsync(ct);
        }
    }
}