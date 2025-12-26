using System.Collections.Generic;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.PowiazaniaZlecenia.Queries.GetPowiazaniaZlecenia
{
    public sealed class GetPowiazaniaZleceniaQuery : IRequest<List<PowiazanieZleceniaDto>>
    {
    }
}