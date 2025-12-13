using CastlePlus2.Domain.Entities.Finanse;

namespace CastlePlus2.Application.Interfaces.Finanse
{
    public interface IPlatnoscRepository
    {
        Task<Platnosc?> GetByIdAsync(long id, CancellationToken ct);
        Task AddAsync(Platnosc entity, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
