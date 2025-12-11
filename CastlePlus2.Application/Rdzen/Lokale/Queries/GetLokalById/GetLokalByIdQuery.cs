using System;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Lokale.Queries.GetLokalById
{
    /// <summary>
    /// Zapytanie o pojedynczy lokal po Id.
    /// </summary>
    public class GetLokalByIdQuery : IRequest<LokalDto?>
    {
        public Guid Id { get; }

        public GetLokalByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
