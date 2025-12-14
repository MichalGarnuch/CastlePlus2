using System.Collections.Generic;
using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Kontakty.Queries.GetKontaktyByPodmiotId
{
    public class GetKontaktyByPodmiotIdQuery : IRequest<List<KontaktDto>>
    {
        public long IdPodmiotu { get; }

        public GetKontaktyByPodmiotIdQuery(long idPodmiotu)
        {
            IdPodmiotu = idPodmiotu;
        }
    }
}
