using MediatR;
using CastlePlus2.Contracts.DTOs.Slowniki;

namespace CastlePlus2.Application.Slowniki.Waluty.Queries.GetWalutaByKod
{
    public class GetWalutaByKodQuery : IRequest<WalutaDto?>
    {
        public string KodWaluty { get; set; } = default!;
    }
}
