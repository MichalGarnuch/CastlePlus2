using CastlePlus2.Domain.Entities.Finanse;

namespace CastlePlus2.Application.Interfaces.Finanse
{
    public interface IFakturaRepository
    {
        Task<Faktura?> GetByIdAsync(long idFaktury, CancellationToken ct);
        Task<bool> ExistsByNumerAsync(string numerFaktury, CancellationToken ct);

        Task AddAsync(Faktura entity, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
