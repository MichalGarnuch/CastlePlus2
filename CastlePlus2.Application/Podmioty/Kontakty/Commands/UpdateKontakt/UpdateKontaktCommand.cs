using MediatR;

namespace CastlePlus2.Application.Podmioty.Kontakty.Commands.UpdateKontakt
{
    public class UpdateKontaktCommand : IRequest<bool>
    {
        public long IdKontaktu { get; set; }
        public long IdPodmiotu { get; set; }
        public string Rodzaj { get; set; } = string.Empty;
        public string Wartosc { get; set; } = string.Empty;
    }
}