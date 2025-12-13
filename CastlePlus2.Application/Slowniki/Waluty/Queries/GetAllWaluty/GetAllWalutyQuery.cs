using MediatR;
using CastlePlus2.Contracts.DTOs.Slowniki;

namespace CastlePlus2.Application.Slowniki.Waluty.Queries.GetAllWaluty
{
    public class GetAllWalutyQuery : IRequest<List<WalutaDto>>
    {
    }
}
