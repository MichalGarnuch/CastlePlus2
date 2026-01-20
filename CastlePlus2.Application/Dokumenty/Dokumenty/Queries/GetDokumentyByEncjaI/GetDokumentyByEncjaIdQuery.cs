using CastlePlus2.Contracts.DTOs.Dokumenty;
using MediatR;
using System;

namespace CastlePlus2.Application.Dokumenty.Dokumenty.Queries.GetDokumentyByEncjaId
{
    public record GetDokumentyByEncjaIdQuery(Guid IdEncji) : IRequest<List<DokumentDto>>;
}