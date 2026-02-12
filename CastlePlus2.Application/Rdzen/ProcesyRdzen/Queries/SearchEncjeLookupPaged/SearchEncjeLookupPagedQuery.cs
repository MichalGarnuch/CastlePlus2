using CastlePlus2.Contracts.DTOs.Common;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.ProcesyRdzen.Queries.SearchEncjeLookupPaged
{
    public sealed record SearchEncjeLookupPagedQuery(
        string? TypEncji,
        string? Q,
        int Page,
        int PageSize
    ) : IRequest<PagedResultDto<EncjaLookupDto>>;
}