using CastlePlus2.Contracts.DTOs.Common;
using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.Requests.Podmioty;

namespace CastlePlus2.Client.Services.Podmioty
{
    public interface IPodmiotyService
    {
        Task<List<PodmiotDto>> GetAllAsync(CancellationToken ct = default);
        Task<PodmiotPagedResultDto> GetPagedAsync(int page, int pageSize, string? searchTerm, string? sortBy, bool sortDesc, CancellationToken ct = default);
        Task<List<PodmiotDto>> SearchAsync(string searchTerm, int take, CancellationToken ct = default);
        Task<PagedResultDto<PodmiotLookupDto>> SearchLookupPagedAsync(
            string? q,
            int page,
            int pageSize,
            CancellationToken ct = default);
        Task<PodmiotDto?> GetByIdAsync(long id, CancellationToken ct = default);

        Task<PodmiotDto> CreateAsync(CreatePodmiotRequest request, CancellationToken ct = default);
        Task<bool> UpdateAsync(long id, UpdatePodmiotRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(long id, CancellationToken ct = default);
    }
}
