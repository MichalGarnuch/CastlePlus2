using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;

namespace CastlePlus2.Client.Services.Finanse
{
    public interface IPozycjeKosztowService
    {
        Task<List<PozycjaKosztuDto>> GetAllAsync(CancellationToken ct = default);
        Task<PozycjaKosztuDto?> GetByIdAsync(long id, CancellationToken ct = default);

        Task<PozycjaKosztuDto> CreateAsync(CreatePozycjaKosztuRequest request, CancellationToken ct = default);
        Task<PozycjaKosztuDto?> UpdateAsync(long id, UpdatePozycjaKosztuRequest request, CancellationToken ct = default);
        Task DeleteAsync(long id, CancellationToken ct = default);
    }
}
