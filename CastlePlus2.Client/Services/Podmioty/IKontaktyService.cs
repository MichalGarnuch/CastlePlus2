using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.Requests.Podmioty;

namespace CastlePlus2.Client.Services.Podmioty
{
    public interface IKontaktyService
    {
        Task<List<KontaktDto>> GetByPodmiotIdAsync(long idPodmiotu, CancellationToken ct = default);
        Task<KontaktDto?> GetByIdAsync(long idKontaktu, CancellationToken ct = default);
        Task<long> CreateAsync(CreateKontaktRequest request, CancellationToken ct = default);
        Task<bool> UpdateAsync(long idKontaktu, UpdateKontaktRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(long idKontaktu, CancellationToken ct = default);
    }
}