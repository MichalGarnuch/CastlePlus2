using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;

namespace CastlePlus2.Client.Services.Rdzen
{
    public interface IProcesyRdzenService
    {
        Task<PrzypisanieAdresuContextDto> GetPrzypisanieAdresuContextAsync(CancellationToken ct = default);
        Task<PrzypiszAdresResultDto> PrzypiszAdresAsync(PrzypiszAdresRequest request, CancellationToken ct = default);
        Task<List<EncjaLookupDto>> SearchEncjeLookupAsync(string? typEncji, string? q, int take = 50, CancellationToken ct = default);

    }
}