using CastlePlus2.Domain.Entities.Finanse;

namespace CastlePlus2.Application.Interfaces.Finanse
{
    public interface IKategoriaKosztuRepository
    {
        Task<KategoriaKosztu?> GetByIdAsync(long id, CancellationToken ct);
        Task<KategoriaKosztu?> GetForUpdateAsync(long id, CancellationToken ct);
        Task<List<KategoriaKosztu>> GetAllAsync(CancellationToken ct);

        Task<bool> ExistsByKodAsync(string kod, CancellationToken ct);
        Task<bool> ExistsOtherByKodAsync(string kod, long excludeIdKategoriiKosztu, CancellationToken ct);

        Task AddAsync(KategoriaKosztu entity, CancellationToken ct);
        Task RemoveAsync(KategoriaKosztu entity, CancellationToken ct);

        Task SaveChangesAsync(CancellationToken ct);
    }
}
