using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Nieruchomosci.Commands.UpdateNieruchomosc
{
    public sealed record UpdateNieruchomoscCommand(Guid Id, UpdateNieruchomoscRequest Request) : IRequest<NieruchomoscDto?>;
}
