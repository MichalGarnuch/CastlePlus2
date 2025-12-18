using CastlePlus2.Domain.Entities.Slowniki;

namespace CastlePlus2.Application.Interfaces.Slowniki
{
    public interface IJednostkaMiaryRepository
    {
        Task<List<JednostkaMiary>> GetAllAsync(CancellationToken ct);
        Task<JednostkaMiary?> GetByKodAsync(string kodJednostki, CancellationToken ct);

        Task<bool> ExistsAsync(string kodJednostki, CancellationToken ct);

        Task AddAsync(JednostkaMiary entity, CancellationToken ct);
        void Remove(JednostkaMiary entity);

        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}
