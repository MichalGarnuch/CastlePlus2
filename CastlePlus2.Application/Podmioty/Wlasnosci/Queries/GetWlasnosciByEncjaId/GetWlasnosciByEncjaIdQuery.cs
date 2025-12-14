using System;
using System.Collections.Generic;
using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Wlasnosci.Queries.GetWlasnosciByEncjaId
{
    public class GetWlasnosciByEncjaIdQuery : IRequest<IReadOnlyList<WlasnoscDto>>
    {
        public Guid IdEncji { get; }

        public GetWlasnosciByEncjaIdQuery(Guid idEncji)
        {
            IdEncji = idEncji;
        }
    }
}
