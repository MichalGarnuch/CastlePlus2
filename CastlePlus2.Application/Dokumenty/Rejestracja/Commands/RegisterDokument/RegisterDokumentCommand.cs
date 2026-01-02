using MediatR;

using CastlePlus2.Contracts.DTOs.Dokumenty;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.Rejestracja.Commands.RegisterDokument
{
    public class RegisterDokumentCommand : IRequest<RegisterDokumentResultDto>
    {
        public Guid IdEncji { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public string? Opis { get; set; }
        public string? SciezkaPliku { get; set; }
    }
}