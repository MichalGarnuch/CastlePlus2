using MediatR;
using System;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUI.Commands.DeleteZasobUI
{
    public record DeleteZasobUICommand(Guid IdEncji) : IRequest<bool>;
}