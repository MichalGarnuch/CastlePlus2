using System;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.ProcesyRdzen.Commands.PrzypiszAdres
{
    public sealed record PrzypiszAdresCommand(
        Guid IdEncji,
        long IdAdresu,
        DateOnly OdDnia,
        DateOnly? DoDnia
    ) : IRequest<PrzypiszAdresResultDto>;
}