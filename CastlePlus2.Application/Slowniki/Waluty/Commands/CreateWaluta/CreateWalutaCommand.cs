using MediatR;
using CastlePlus2.Contracts.DTOs.Slowniki;

namespace CastlePlus2.Application.Slowniki.Waluty.Commands.CreateWaluta
{
    public class CreateWalutaCommand : IRequest<WalutaDto>
    {
        public string KodWaluty { get; set; } = default!;
        public string Nazwa { get; set; } = default!;
    }
}
