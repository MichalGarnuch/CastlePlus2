using MediatR;

namespace CastlePlus2.Application.Finanse.PozycjeKosztow.Commands.DeletePozycjaKosztu
{
    public sealed record DeletePozycjaKosztuCommand(long IdPozycjiKosztu) : IRequest<bool>;
}