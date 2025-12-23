using MediatR;

namespace CastlePlus2.Application.Podmioty.Kontakty.Commands.DeleteKontakt
{
    public class DeleteKontaktCommand : IRequest<bool>
    {
        public DeleteKontaktCommand(long idKontaktu)
        {
            IdKontaktu = idKontaktu;
        }

        public long IdKontaktu { get; }
    }
}