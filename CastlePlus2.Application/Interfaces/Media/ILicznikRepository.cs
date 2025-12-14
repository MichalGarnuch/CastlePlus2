using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Domain.Entities.Media;

namespace CastlePlus2.Application.Interfaces.Media
{
    public interface ILicznikRepository
    {
        Task<Licznik?> GetByIdAsync(long idLicznika, CancellationToken ct);

        Task<bool> NumerExistsAsync(string numerNV, CancellationToken ct);
        Task<bool> PrzylaczeExistsAsync(long idPrzylacza, CancellationToken ct);
        Task<bool> JednostkaExistsAsync(string kodJednostki, CancellationToken ct);
        Task<bool> LicznikExistsAsync(long idLicznika, CancellationToken ct);

        Task AddAsync(Licznik entity, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
