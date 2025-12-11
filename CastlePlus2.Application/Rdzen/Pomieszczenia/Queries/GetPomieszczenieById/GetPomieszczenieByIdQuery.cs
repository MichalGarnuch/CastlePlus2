using System;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Pomieszczenia.Queries.GetPomieszczenieById
{
    public class GetPomieszczenieByIdQuery : IRequest<PomieszczenieDto?>
    {
        public Guid Id { get; set; }
    }
}
