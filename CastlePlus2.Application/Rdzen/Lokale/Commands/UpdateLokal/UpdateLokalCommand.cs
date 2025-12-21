using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Lokale.Commands.UpdateLokal
{
    public sealed record UpdateLokalCommand(Guid Id, UpdateLokalRequest Request) : IRequest<LokalDto?>;
}
