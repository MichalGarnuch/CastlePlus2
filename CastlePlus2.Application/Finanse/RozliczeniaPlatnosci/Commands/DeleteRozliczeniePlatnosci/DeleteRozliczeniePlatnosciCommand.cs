using MediatR;

namespace CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Commands.DeleteRozliczeniePlatnosci
{
    public sealed record DeleteRozliczeniePlatnosciCommand(long IdRozliczenia) : IRequest<bool>;
}