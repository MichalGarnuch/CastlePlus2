using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.Requests.Podmioty;

namespace CastlePlus2.Client.Services.Podmioty
{
    public interface IPodmiotyService
    {
        Task<List<PodmiotDto>> GetAllAsync(CancellationToken ct = default);
        Task<PodmiotDto?> GetByIdAsync(long id, CancellationToken ct = default);

        Task<long> CreateAsync(CreatePodmiotRequest request, CancellationToken ct = default);
        Task<bool> UpdateAsync(long id, UpdatePodmiotRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(long id, CancellationToken ct = default);
    }
}
