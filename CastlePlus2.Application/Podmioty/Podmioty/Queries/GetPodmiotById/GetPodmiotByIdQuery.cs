using MediatR;
using CastlePlus2.Contracts.DTOs.Podmioty;

namespace CastlePlus2.Application.Podmioty.Podmioty.Queries.GetPodmiotById
{
    public class GetPodmiotByIdQuery : IRequest<PodmiotDto?>
    {
        public long IdPodmiotu { get; set; }
    }
}
