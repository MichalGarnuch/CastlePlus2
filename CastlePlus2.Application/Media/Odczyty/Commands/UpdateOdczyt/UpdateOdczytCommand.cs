using CastlePlus2.Contracts.DTOs.Media;
using CastlePlus2.Contracts.Requests.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Odczyty.Commands.UpdateOdczyt
{
    public record UpdateOdczytCommand(long IdOdczytu, UpdateOdczytRequest Request) : IRequest<OdczytDto?>;
}
