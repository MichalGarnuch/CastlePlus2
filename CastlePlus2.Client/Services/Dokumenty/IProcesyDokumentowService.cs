using CastlePlus2.Contracts.DTOs.Dokumenty;
using CastlePlus2.Contracts.Requests.Dokumenty;

namespace CastlePlus2.Client.Services.Dokumenty
{
    public interface IProcesyDokumentowService
    {
        Task<RegisterDokumentContextDto> GetRegisterContextAsync(CancellationToken ct = default);
        Task<RegisterDokumentResultDto> RegisterAsync(RegisterDokumentRequest request, CancellationToken ct = default);
    }
}