using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.KategorieKosztow.Commands.UpdateKategoriaKosztu
{
    public class UpdateKategoriaKosztuCommand : IRequest<KategoriaKosztuDto?>
    {
        public long IdKategoriiKosztu { get; set; }
        public string Kod { get; set; } = string.Empty;
        public string Nazwa { get; set; } = string.Empty;
    }
}
