using MediatR;

namespace CastlePlus2.Application.Utrzymanie.PowiazaniaZlecenia.Commands.DeletePowiazanieZlecenia
{
    public sealed class DeletePowiazanieZleceniaCommand : IRequest<bool>
    {
        public long IdPowiazania { get; }

        public DeletePowiazanieZleceniaCommand(long idPowiazania)
        {
            IdPowiazania = idPowiazania;
        }
    }
}