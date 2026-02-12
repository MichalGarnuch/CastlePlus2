using CastlePlus2.Contracts.DTOs.Common;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;

namespace CastlePlus2.Client.Services.Najem
{
    public interface IPrzedmiotyNajmuService
    {
        Task<List<PrzedmiotNajmuDto>> GetAllAsync(CancellationToken ct = default);
        Task<PrzedmiotNajmuDto?> GetByIdAsync(long id, CancellationToken ct = default);
        Task<PagedResultDto<PrzedmiotNajmuLookupDto>> SearchLookupPagedAsync(
            string? q,
            Guid? idUmowyNajmu,
            int page,
            int pageSize,
            CancellationToken ct = default);
        Task<PrzedmiotNajmuDto> CreateAsync(CreatePrzedmiotNajmuRequest request, CancellationToken ct = default);
        Task<bool> UpdateAsync(long id, UpdatePrzedmiotNajmuRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(long id, CancellationToken ct = default);
    }
}