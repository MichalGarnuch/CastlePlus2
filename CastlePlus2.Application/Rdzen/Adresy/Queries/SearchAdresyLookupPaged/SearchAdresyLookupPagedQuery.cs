using CastlePlus2.Contracts.DTOs.Common;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Adresy.Queries.SearchAdresyLookupPaged
{
    public sealed record SearchAdresyLookupPagedQuery(
        string? Q,
        int Page,
        int PageSize
    ) : IRequest<PagedResultDto<AdresLookupDto>>;
}