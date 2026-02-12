using CastlePlus2.Contracts.DTOs.Common;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CastlePlus2.Client.Services.Najem
{
    public interface IUmowyNajmuService
    {
        Task<UmowaNajmuContextDto> GetContextAsync(CancellationToken ct = default);
        Task<List<UmowaNajmuDto>> GetAllAsync(CancellationToken ct = default);
        Task<UmowaNajmuDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<PagedResultDto<UmowaNajmuLookupDto>> SearchLookupPagedAsync(
            string? q,
            int page,
            int pageSize,
            CancellationToken ct = default);
        Task<UmowaNajmuDto> CreateAsync(CreateUmowaNajmuRequest request, CancellationToken ct = default);
        Task<bool> UpdateAsync(Guid id, UpdateUmowaNajmuRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
        Task<ZawrzUmoweNajmuResult> ZawrzAsync(ZawrzUmoweNajmuRequest request, CancellationToken ct = default);
    }
}