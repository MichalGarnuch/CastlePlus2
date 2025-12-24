using MediatR;

namespace CastlePlus2.Application.Finanse.PozycjeKosztow.Commands.DeletePozycjaKosztu
{
    public class DeletePozycjaKosztuCommand : IRequest
    {
        public long IdPozycjiKosztu { get; set; }
    }
}
