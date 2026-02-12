using CastlePlus2.Contracts.DTOs.Common;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Liczniki.Queries.SearchLicznikiLookupPaged
{
    public sealed record SearchLicznikiLookupPagedQuery(
        string? Q,
        int Page,
        int PageSize
    ) : IRequest<PagedResultDto<LicznikLookupDto>>;
}