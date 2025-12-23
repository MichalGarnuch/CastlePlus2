using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;

namespace CastlePlus2.Client.Services.Finanse
{
    public interface IKategorieKosztowService
    {
        Task<List<KategoriaKosztuDto>> GetAllAsync(CancellationToken ct = default);
        Task<KategoriaKosztuDto?> GetByIdAsync(long id, CancellationToken ct = default);

        Task<KategoriaKosztuDto> CreateAsync(CreateKategoriaKosztuRequest request, CancellationToken ct = default);
        Task<KategoriaKosztuDto?> UpdateAsync(long id, UpdateKategoriaKosztuRequest request, CancellationToken ct = default);
        Task DeleteAsync(long id, CancellationToken ct = default);
    }
}
