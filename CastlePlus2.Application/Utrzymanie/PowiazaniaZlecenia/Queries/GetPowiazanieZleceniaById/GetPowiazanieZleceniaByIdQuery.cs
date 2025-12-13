using CastlePlus2.Contracts.DTOs.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.PowiazaniaZlecenia.Queries.GetPowiazanieZleceniaById
{
    public class GetPowiazanieZleceniaByIdQuery : IRequest<PowiazanieZleceniaDto?>
    {
        public long IdPowiazania { get; }

        public GetPowiazanieZleceniaByIdQuery(long idPowiazania)
        {
            IdPowiazania = idPowiazania;
        }
    }
}
