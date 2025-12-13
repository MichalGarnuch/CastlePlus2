using MediatR;
using CastlePlus2.Contracts.DTOs.Podmioty;

namespace CastlePlus2.Application.Podmioty.Podmioty.Queries.GetAllPodmioty
{
    public class GetAllPodmiotyQuery : IRequest<List<PodmiotDto>>
    {
    }
}
