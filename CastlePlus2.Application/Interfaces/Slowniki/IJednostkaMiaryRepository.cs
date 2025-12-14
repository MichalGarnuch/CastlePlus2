using CastlePlus2.Domain.Entities.Slowniki;

namespace CastlePlus2.Application.Interfaces.Slowniki
{
    public interface IJednostkaMiaryRepository
    {
        Task AddAsync(JednostkaMiary entity, CancellationToken ct);
        Task<JednostkaMiary?> GetByKodAsync(string kod, CancellationToken ct);
        Task<List<JednostkaMiary>> GetAllAsync(CancellationToken ct);
        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}
