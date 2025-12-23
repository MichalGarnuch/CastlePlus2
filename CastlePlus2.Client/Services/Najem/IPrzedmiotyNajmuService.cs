using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;

namespace CastlePlus2.Client.Services.Najem
{
    public interface IPrzedmiotyNajmuService
    {
        Task<List<PrzedmiotNajmuDto>> GetAllAsync(CancellationToken ct = default);
        Task<PrzedmiotNajmuDto?> GetByIdAsync(long id, CancellationToken ct = default);
        Task<PrzedmiotNajmuDto> CreateAsync(CreatePrzedmiotNajmuRequest request, CancellationToken ct = default);
        Task<PrzedmiotNajmuDto?> UpdateAsync(long id, UpdatePrzedmiotNajmuRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(long id, CancellationToken ct = default);
    }
}