using MediatR;
using CastlePlus2.Contracts.DTOs.Slowniki;

namespace CastlePlus2.Application.Slowniki.Waluty.Commands.UpdateWaluta
{
    public class UpdateWalutaCommand : IRequest<WalutaDto?>
    {
        public string KodWaluty { get; set; } = string.Empty;
        public string Nazwa { get; set; } = string.Empty;
    }
}
