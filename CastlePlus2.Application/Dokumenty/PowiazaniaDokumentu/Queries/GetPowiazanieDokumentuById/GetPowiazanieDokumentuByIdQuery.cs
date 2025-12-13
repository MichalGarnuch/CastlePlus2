using CastlePlus2.Contracts.DTOs.Dokumenty;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.PowiazaniaDokumentu.Queries.GetPowiazanieDokumentuById
{
    public class GetPowiazanieDokumentuByIdQuery : IRequest<PowiazanieDokumentuDto?>
    {
        public long IdPowiazania { get; }

        public GetPowiazanieDokumentuByIdQuery(long idPowiazania)
        {
            IdPowiazania = idPowiazania;
        }
    }
}
