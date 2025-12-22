using System.Collections.Generic;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Pomieszczenia.Queries.GetAllPomieszczenia
{
    public class GetAllPomieszczeniaQuery : IRequest<List<PomieszczenieDto>>
    {
    }
}
