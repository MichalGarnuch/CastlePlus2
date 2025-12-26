using CastlePlus2.Contracts.DTOs.Dokumenty;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.PowiazaniaDokumentu.Queries.GetAllPowiazaniaDokumentu
{
    public record GetAllPowiazaniaDokumentuQuery() : IRequest<List<PowiazanieDokumentuDto>>;
}
