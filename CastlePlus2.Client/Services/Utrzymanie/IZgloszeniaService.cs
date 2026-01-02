using CastlePlus2.Contracts.DTOs.Utrzymanie;
using CastlePlus2.Contracts.Requests.Utrzymanie;

namespace CastlePlus2.Client.Services.Utrzymanie
{
    public interface IZgloszeniaService
    {
        Task<ZglosUsterkeContextDto> GetContextAsync(CancellationToken ct = default);
        Task<ZglosUsterkeResult> CreateAsync(ZglosUsterkeRequest request, CancellationToken ct = default);
    }
}