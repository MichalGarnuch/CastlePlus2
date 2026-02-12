using CastlePlus2.Contracts.DTOs.Common;
using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Podmioty.Queries.SearchPodmiotyLookupPaged
{
    public sealed record SearchPodmiotyLookupPagedQuery(
        string? Q,
        int Page,
        int PageSize
    ) : IRequest<PagedResultDto<PodmiotLookupDto>>;
}