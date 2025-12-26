using CastlePlus2.Contracts.DTOs.Dokumenty;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.Dokumenty.Queries.GetAllDokumenty
{
    public record GetAllDokumentyQuery() : IRequest<List<DokumentDto>>;
}
