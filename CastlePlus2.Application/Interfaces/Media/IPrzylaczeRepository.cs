using System;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Media;

namespace CastlePlus2.Application.Interfaces.Media
{
    public interface IPrzylaczeRepository
    {
        Task<Przylacze?> GetByIdAsync(long idPrzylacza, CancellationToken ct);
        Task AddAsync(Przylacze entity, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);

        // Walidacja FK do Encji (żeby kontroler nie robił “magii”)
        Task<bool> EncjaExistsAsync(Guid idEncji, CancellationToken ct);
    }
}
