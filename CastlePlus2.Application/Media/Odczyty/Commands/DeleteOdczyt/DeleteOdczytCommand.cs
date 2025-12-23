using MediatR;

namespace CastlePlus2.Application.Media.Odczyty.Commands.DeleteOdczyt
{
    public record DeleteOdczytCommand(long IdOdczytu) : IRequest<bool>;
}
