using CastlePlus2.Contracts.DTOs.Slowniki;
using CastlePlus2.Contracts.Requests.Slownik;

namespace CastlePlus2.Client.Services.Slowniki
{
    public interface IJednostkiMiaryService
    {
        Task<List<JednostkaMiaryDto>> GetAllAsync(CancellationToken ct = default);
        Task<JednostkaMiaryDto?> GetByKodAsync(string kodJednostki, CancellationToken ct = default);

        Task<JednostkaMiaryDto> CreateAsync(CreateJednostkaMiaryRequest request, CancellationToken ct = default);
        Task UpdateAsync(string kodJednostki, UpdateJednostkaMiaryRequest request, CancellationToken ct = default);
        Task DeleteAsync(string kodJednostki, CancellationToken ct = default);
    }
}
