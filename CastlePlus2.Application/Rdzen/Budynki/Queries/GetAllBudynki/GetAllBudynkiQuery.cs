using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Budynki.Queries.GetAllBudynki
{
    public sealed record GetAllBudynkiQuery : IRequest<List<BudynekDto>>;
}
