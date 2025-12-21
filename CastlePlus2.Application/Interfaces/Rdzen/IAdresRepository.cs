using CastlePlus2.Domain.Entities.Rdzen;

namespace CastlePlus2.Application.Interfaces.Rdzen
{
    public interface IAdresRepository
    {
        Task<Adres?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<List<Adres>> GetAllAsync(CancellationToken cancellationToken = default);

        Task AddAsync(Adres entity, CancellationToken cancellationToken = default);

        // Uwaga: dla Adres (tabela bez RowVersion) robimy klasyczny update przez EF tracking:
        Task UpdateAsync(Adres entity, CancellationToken cancellationToken = default);
        Task RemoveAsync(Adres entity, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
