using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Podmioty.Queries.GetPodmiotyPaged
{
    public class GetPodmiotyPagedQuery : IRequest<PodmiotPagedResultDto>
    {
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 20;
        public string? SearchTerm { get; init; }
        public string? SortBy { get; init; }
        public bool SortDesc { get; init; }
    }
}
