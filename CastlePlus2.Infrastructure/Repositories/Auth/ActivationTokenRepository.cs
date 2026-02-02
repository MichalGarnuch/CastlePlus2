using CastlePlus2.Application.Interfaces.Auth;
using CastlePlus2.Domain.Entities.Auth;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CastlePlus2.Infrastructure.Repositories.Auth
{
    public sealed class ActivationTokenRepository : IActivationTokenRepository
    {
        private readonly CastlePlus2DbContext _dbContext;

        public ActivationTokenRepository(CastlePlus2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(ActivationToken token, CancellationToken ct)
        {
            await _dbContext.ActivationTokens.AddAsync(token, ct);
            await _dbContext.SaveChangesAsync(ct);
        }

        public Task<ActivationToken?> FindByHashAsync(byte[] tokenHash, CancellationToken ct)
        {
            return _dbContext.ActivationTokens
                .FirstOrDefaultAsync(x => x.TokenHash.SequenceEqual(tokenHash) && x.UsedAtUtc == null, ct);
        }

        public async Task MarkUsedAsync(ActivationToken token, DateTime usedAtUtc, CancellationToken ct)
        {
            token.UsedAtUtc = usedAtUtc;
            _dbContext.ActivationTokens.Update(token);
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}