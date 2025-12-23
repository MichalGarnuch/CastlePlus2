using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.Requests.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Kontakty.Commands.UpdateKontakt
{
    public class UpdateKontaktCommand : IRequest<KontaktDto?>
    {
        public long IdKontaktu { get; set; }
        public UpdateKontaktRequest Request { get; set; } = new();
    }
}