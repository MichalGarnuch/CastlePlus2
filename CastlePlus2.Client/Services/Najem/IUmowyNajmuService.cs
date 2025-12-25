using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CastlePlus2.Client.Services.Najem
{
    public interface IUmowyNajmuService
    {
        Task<List<UmowaNajmuDto>> GetAllAsync(CancellationToken ct = default);
        Task<UmowaNajmuDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<Guid> CreateAsync(CreateUmowaNajmuRequest request, CancellationToken ct = default);
        Task<bool> UpdateAsync(Guid id, UpdateUmowaNajmuRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    }
}