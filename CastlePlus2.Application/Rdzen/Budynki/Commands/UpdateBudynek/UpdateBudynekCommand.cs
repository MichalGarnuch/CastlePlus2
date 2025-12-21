using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Budynki.Commands.UpdateBudynek
{
    public sealed record UpdateBudynekCommand(Guid Id, UpdateBudynekRequest Request) : IRequest<BudynekDto?>;
}
