using CastlePlus2.Contracts.DTOs.Slowniki;
using CastlePlus2.Contracts.Requests.Slownik;

namespace CastlePlus2.Client.Services.Slowniki
{
    public interface IIndeksacjeService
    {
        Task<List<IndeksacjaDto>> GetAllAsync(CancellationToken ct = default);
        Task<IndeksacjaDto?> GetByKodAsync(string kod, CancellationToken ct = default);
        Task<IndeksacjaDto> CreateAsync(CreateIndeksacjaRequest request, CancellationToken ct = default);
        Task UpdateAsync(string kod, UpdateIndeksacjaRequest request, CancellationToken ct = default);
        Task DeleteAsync(string kod, CancellationToken ct = default);
    }
}
