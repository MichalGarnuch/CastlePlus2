using CastlePlus2.Contracts.DTOs.Common;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Faktury.Queries.SearchFakturyLookupPaged
{
    public sealed record SearchFakturyLookupPagedQuery(
        string? Q,
        long? IdPodmiotu,
        int Page,
        int PageSize
    ) : IRequest<PagedResultDto<FakturaLookupDto>>;
}