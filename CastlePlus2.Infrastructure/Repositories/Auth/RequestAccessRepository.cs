using CastlePlus2.Application.Interfaces.Auth;
using CastlePlus2.Domain.Entities.Auth;
using CastlePlus2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Infrastructure.Repositories.Auth
{
    public sealed class RequestAccessRepository : IAccessRequestRepository
    {
        private readonly CastlePlus2DbContext _dbContext;

        public RequestAccessRepository(CastlePlus2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(RequestAccess request, CancellationToken ct)
        {
            await _dbContext.RequestAccesses.AddAsync(request, ct);
            await _dbContext.SaveChangesAsync(ct);
            return request.IdRequestAccess;
        }

        public Task<RequestAccess?> GetByIdAsync(int id, CancellationToken ct)
        {
            return _dbContext.RequestAccesses.FirstOrDefaultAsync(x => x.IdRequestAccess == id, ct);
        }

        public Task<List<RequestAccess>> GetByStatusAsync(RequestAccessStatus status, CancellationToken ct)
        {
            return _dbContext.RequestAccesses
                .AsNoTracking()
                .Where(x => x.Status == status)
                .OrderBy(x => x.CreatedAtUtc)
                .ToListAsync(ct);
        }

        public Task<bool> PendingExistsByEmailOrLoginAsync(string email, string? login, CancellationToken ct)
        {
            return _dbContext.RequestAccesses.AnyAsync(
                x => x.Status == RequestAccessStatus.Pending
                     && (x.Email == email || (!string.IsNullOrWhiteSpace(login) && x.Login == login)),
                ct);
        }

        public async Task UpdateAsync(RequestAccess request, CancellationToken ct)
        {
            _dbContext.RequestAccesses.Update(request);
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}