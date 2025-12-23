using CastlePlus2.Domain.Entities.Finanse;

namespace CastlePlus2.Application.Interfaces.Finanse
{
    public interface IFakturaRepository
    {
        Task<List<Faktura>> GetAllAsync(CancellationToken ct);
        Task<Faktura?> GetByIdAsync(long idFaktury, CancellationToken ct);
        Task<Faktura?> GetForUpdateAsync(long idFaktury, CancellationToken ct);

        Task<bool> ExistsByNumerAsync(string numerFaktury, CancellationToken ct);
        Task<bool> ExistsByNumerAsync(string numerFaktury, long excludeIdFaktury, CancellationToken ct);

        Task AddAsync(Faktura entity, CancellationToken ct);
        void Remove(Faktura entity);

        Task SaveChangesAsync(CancellationToken ct);
    }
}
