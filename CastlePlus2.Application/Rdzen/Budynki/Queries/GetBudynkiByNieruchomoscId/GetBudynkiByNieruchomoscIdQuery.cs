using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Budynki.Queries.GetBudynkiByNieruchomoscId
{
    public sealed record GetBudynkiByNieruchomoscIdQuery(Guid IdNieruchomosci) : IRequest<List<BudynekDto>>;
}
