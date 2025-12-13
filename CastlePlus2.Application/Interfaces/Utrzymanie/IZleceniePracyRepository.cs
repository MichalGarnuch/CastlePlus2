using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Utrzymanie;

namespace CastlePlus2.Application.Interfaces.Utrzymanie
{
    /// <summary>
    /// Interfejs repozytorium dla [utrzymanie].[ZleceniePracy].
    /// Warstwa Application nie zna EF – tylko kontrakt.
    /// </summary>
    public interface IZleceniePracyRepository
    {
        Task<ZleceniePracy?> GetByIdAsync(long idZlecenia, CancellationToken cancellationToken = default);
        Task AddAsync(ZleceniePracy entity, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
