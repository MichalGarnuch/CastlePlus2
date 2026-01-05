using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.ProcesyRdzen.Queries.SearchEncjeLookup
{
    public sealed record SearchEncjeLookupQuery(
        string? TypEncji,
        string? Q,
        int Take = 50
    ) : IRequest<List<EncjaLookupDto>>;
}
