using CastlePlus2.Contracts.DTOs.Utrzymanie;
using CastlePlus2.Contracts.Requests.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.PowiazaniaZlecenia.Commands.UpdatePowiazanieZlecenia
{
    public sealed record UpdatePowiazanieZleceniaCommand(long IdPowiazania, UpdatePowiazanieZleceniaRequest Request) : IRequest<PowiazanieZleceniaDto?>;
}