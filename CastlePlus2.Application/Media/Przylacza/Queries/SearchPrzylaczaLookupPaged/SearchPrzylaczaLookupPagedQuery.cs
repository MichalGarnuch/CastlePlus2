using CastlePlus2.Contracts.DTOs.Common;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Przylacza.Queries.SearchPrzylaczaLookupPaged
{
    public sealed record SearchPrzylaczaLookupPagedQuery(
        string? Q,
        int Page,
        int PageSize
    ) : IRequest<PagedResultDto<PrzylaczeLookupDto>>;
}