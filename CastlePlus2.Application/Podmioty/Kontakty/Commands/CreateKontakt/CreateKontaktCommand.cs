using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Kontakty.Commands.CreateKontakt
{
    public class CreateKontaktCommand : IRequest<KontaktDto>
    {
        public long IdPodmiotu { get; set; }
        public string Rodzaj { get; set; } = string.Empty;
        public string Wartosc { get; set; } = string.Empty;
    }
}
