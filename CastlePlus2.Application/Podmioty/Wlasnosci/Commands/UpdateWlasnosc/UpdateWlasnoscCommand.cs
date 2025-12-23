using CastlePlus2.Contracts.Requests.Podmioty;
using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Wlasnosci.Commands.UpdateWlasnosc
{
    public sealed record UpdateWlasnoscCommand(long IdWlasnosci, UpdateWlasnoscRequest Request) : IRequest<WlasnoscDto?>;
}