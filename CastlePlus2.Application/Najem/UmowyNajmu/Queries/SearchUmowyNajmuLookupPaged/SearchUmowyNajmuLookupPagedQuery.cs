using CastlePlus2.Contracts.DTOs.Common;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.UmowyNajmu.Queries.SearchUmowyNajmuLookupPaged
{
    public sealed record SearchUmowyNajmuLookupPagedQuery(
        string? Q,
        int Page,
        int PageSize
    ) : IRequest<PagedResultDto<UmowaNajmuLookupDto>>;
}