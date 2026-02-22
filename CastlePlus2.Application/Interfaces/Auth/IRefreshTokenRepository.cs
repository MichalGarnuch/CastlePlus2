using System;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Auth;

namespace CastlePlus2.Application.Interfaces.Auth
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token, CancellationToken ct);
        Task<RefreshToken?> FindByTokenHashAsync(byte[] tokenHash, CancellationToken ct);
        Task RevokeAsync(long idRefreshToken, DateTime revokedAtUtc, CancellationToken ct);
        Task RevokeAllForUserAsync(int userId, DateTime revokedAtUtc, CancellationToken ct);
    }
}
