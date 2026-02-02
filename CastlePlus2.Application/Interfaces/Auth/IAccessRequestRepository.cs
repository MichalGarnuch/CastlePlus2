using CastlePlus2.Domain.Entities.Auth;

namespace CastlePlus2.Application.Interfaces.Auth
{
    public interface IAccessRequestRepository
    {
        Task<int> CreateAsync(RequestAccess request, CancellationToken ct);
        Task<RequestAccess?> GetByIdAsync(int id, CancellationToken ct);
        Task<List<RequestAccess>> GetByStatusAsync(RequestAccessStatus status, CancellationToken ct);
        Task<bool> PendingExistsByEmailOrLoginAsync(string email, string? login, CancellationToken ct);
        Task UpdateAsync(RequestAccess request, CancellationToken ct);
    }
}