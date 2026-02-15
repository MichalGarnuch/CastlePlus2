using CastlePlus2.Application.Finanse.ProcesyFaktury.Commands.WystawFakture;
using CastlePlus2.Contracts.DTOs.Finanse;

namespace CastlePlus2.Application.Finanse.ProcesyFaktury.Common
{
    public interface IFakturaCreationService
    {
        Task<WystawFaktureResultDto> CreateAsync(WystawFaktureCommand request, CancellationToken ct);
    }
}