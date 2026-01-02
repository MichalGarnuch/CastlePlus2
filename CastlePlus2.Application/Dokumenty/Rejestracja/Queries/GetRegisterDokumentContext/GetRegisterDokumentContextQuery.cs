using CastlePlus2.Contracts.DTOs.Dokumenty;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.Rejestracja.Queries.GetRegisterDokumentContext
{
    public record GetRegisterDokumentContextQuery() : IRequest<RegisterDokumentContextDto>;
}