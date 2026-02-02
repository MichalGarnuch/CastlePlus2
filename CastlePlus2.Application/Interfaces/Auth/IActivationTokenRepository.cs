using CastlePlus2.Domain.Entities.Auth;

namespace CastlePlus2.Application.Interfaces.Auth
{
    public interface IActivationTokenRepository
    {
        Task AddAsync(ActivationToken token, CancellationToken ct);
        Task<ActivationToken?> FindByHashAsync(byte[] tokenHash, CancellationToken ct);
        Task MarkUsedAsync(ActivationToken token, DateTime usedAtUtc, CancellationToken ct);
    }
}